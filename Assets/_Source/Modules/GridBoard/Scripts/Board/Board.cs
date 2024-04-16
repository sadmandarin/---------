using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace GridBoard
{
    internal class Board : MonoBehaviour
    {
        public Action<bool> EnteredSlotForUpgrade;
        public Action<IUnitHolder> ChangedSelectedSlot;
        public List<BattleSlot> BattleSlots => _battleSlots;
        public int FreeSpotsCount => _battleSlots.Where(n => n.UnitHolder.HasUnit == false && n.IsActive).Count();
        public int OccupiedSpotsCount => _battleSlots.Where(n => n.UnitHolder.HasUnit != false && n.IsActive).Count();
        public Vector2 FreeSpotPosition => _battleSlots.First(n => n.UnitHolder.HasUnit == false && n.IsActive).Position;

        [SerializeField] private BattleSlot _battleSlotPrefab;
        [SerializeField] private Transform _parentForBattleSlots;
        [SerializeField] private GridInputManager _inputManager;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private AudioSource _upgradeSound;
        [SerializeField] private BoardUnitsPersistentHolder _boardUnitsPersistentHolder;
        [SerializeField] private HeroPlacement _heroPlacement;

        private List<BattleSlot> _battleSlots = new List<BattleSlot>();
        private List<BattleSlot> _slotsForNextUpgrade;

        private const int MaxWidth = 6, MaxHeight = 6;

        internal void Init(int width, int height)
        {
            int[,] activeSlots = BoardSizeConfig.GetActiveSlots(width, height);
            for (int i = MaxWidth; i >= 0; i--)
            {
                for (int j = MaxHeight; j >= 0; j--)
                {
                    var battleSlot = Instantiate(_battleSlotPrefab, _parentForBattleSlots);
                    battleSlot.transform.localPosition = new Vector3(i, 0, j);
                    bool isSlotActive = activeSlots[i, j] == 1;
                    battleSlot.Init(new Vector2(i, j), isSlotActive);
                    _battleSlots.Add(battleSlot);
                }
            }
            int maxActiveWidth = (int)_battleSlots.Where(n => n.IsActive).Max(n => n.Position.x);
            int maxActiveHeight = (int)_battleSlots.Where(n => n.IsActive).Max(n => n.Position.y);
            int minActiveHeight = (int)_battleSlots.Where(n => n.IsActive).Min(n => n.Position.y);
            _heroPlacement.transform.localPosition = new Vector3(maxActiveWidth + 1, 0,
                                                                 minActiveHeight + (maxActiveHeight - minActiveHeight) * 0.5f);
        }

        

        internal void AddToFreeSpot(IBoardUnit unit)
        {
            var freeSpot = _battleSlots.FirstOrDefault(n => n.Position == unit.Position);
            freeSpot.Load(unit);
            _boardUnitsPersistentHolder.AddUnit(new BoardUnitData(unit.Name, unit.Level, unit.Position));
        }

        internal void RemoveUnitFromBoard(IBoardUnit boardUnit)
        {
            var battleSlot = GetBattleSlotByPosition(boardUnit.Position);
            _boardUnitsPersistentHolder.RemoveUnit(new BoardUnitData(boardUnit.Name, boardUnit.Level, boardUnit.Position));
            battleSlot.DestroyUnit();
        }

        internal void Load(IBoardUnit[] boardUnits)
        {
            foreach (var unit in boardUnits)
            {
                var battleSlot = _battleSlots.FirstOrDefault(n => n.Position == unit.Position);
                if (battleSlot == null)
                    return;
                //if (battleSlot.IsActive == false)
                    
                battleSlot.Load(unit);
            }
        }

        internal void FindNextUpgrade(IBoardUnit selectedBoardUnit, int maxLevel)
        {
            if (selectedBoardUnit.Level == maxLevel)
                return;

            var battleSlot = _battleSlots.Where(n => n.IsActive &&
                                                            n.UnitHolder.Unit != null && n.UnitHolder.Unit.Name == selectedBoardUnit.Name 
                                                            && n.UnitHolder.Unit.Level == selectedBoardUnit.Level
                                                            && n.UnitHolder.Unit.Position != selectedBoardUnit.Position).ToList();
            if (battleSlot != null)
                ActivateSlotsForNextUpgrade(battleSlot);
        }

        private void ActivateSlotsForNextUpgrade(List<BattleSlot> battleSlots)
        {
            foreach (BattleSlot slot in battleSlots)
            {
                slot.ToggleAvailableEffect(true);
            }
            _slotsForNextUpgrade = battleSlots;
        }

        internal void ActivateUpgradeEffect(BattleSlot battleSlot)
        {
            int index = _battleSlots.IndexOf(battleSlot);
            if (index < 0) return;
            _battleSlots[index].PlayUpgradeEffect();
            _upgradeSound.Play();
        }

        internal void HiglightActiveSlot()
        {
            Ray ray = _inputManager.GetRayUnderMouse();
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
            {
                if (hit.transform.TryGetComponent(out BattleSlot battleSlot))
                {
                    if (battleSlot.IsActive)
                        SelectBattleSlot(battleSlot);
                }
            }
        }

        private void SelectBattleSlot(BattleSlot battleSlot)
        {
            int index = _battleSlots.IndexOf(battleSlot);
            if (index < 0) return;

            UnselectAll();

            _battleSlots[index].Select();
            var canSlotBeUpgraded = _slotsForNextUpgrade != null ? _slotsForNextUpgrade.IndexOf(_battleSlots[index]) >= 0 : false;
            ChangedSelectedSlot?.Invoke(battleSlot.UnitHolder);
            EnteredSlotForUpgrade?.Invoke(canSlotBeUpgraded);
        }

        private void UnselectAll()
        {
            foreach (var slot in _battleSlots)
            {
                if (slot.IsActive)
                    slot.Unselect();
            }
        }

        internal void StopBoardUpdate()
        {
            UnselectAll();
            foreach (var slot in _battleSlots)
            {
                if (slot.IsActive)
                    slot.ToggleAvailableEffect(false);
            }
        }

        internal IUnitHolder GetBattleSlotByPosition(Vector2 position)
        {
            return _battleSlots.Select(n => n.UnitHolder).ToList().FirstOrDefault(n => n.HasUnit && n.Unit.Position == position);
        }

        internal BattleSlot GetBattleSlotByUnitHolder(IUnitHolder unitHolder)
        {
            return _battleSlots.First(n => n.UnitHolder == unitHolder);
        }

        internal List<GameObject> GetAllUnitsOnBoard()
        {
            var battleSlotsWithUnits = _battleSlots.Where(n => n.UnitHolder.HasUnit).ToList();
            var unitGroups = battleSlotsWithUnits.Select(n => n.UnitHolder.Unit.GetAllUnitsInGroup()).ToList();
            List<GameObject> units = new List<GameObject>();
            foreach (var unitGroup in unitGroups)
            {
                foreach (var unit in unitGroup)
                {
                    units.Add(unit);
                }
            }
            return units;
        }

        internal List<IBoardUnit> GetAllBoardUnits()
        {
            return _battleSlots.Where(n => n.UnitHolder.HasUnit).Select(n => n.UnitHolder.Unit).ToList();
        }

        internal void ResetBattleSlots()
        {
            foreach (var battleSlot in _battleSlots)
            {
                Destroy(battleSlot.gameObject);
            }
            _battleSlots.Clear();
        }
    }
}
