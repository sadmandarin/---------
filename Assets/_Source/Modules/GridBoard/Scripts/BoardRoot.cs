using PersistentData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridBoard
{
    public class BoardRoot : MonoBehaviour
    {
        public Action UnitsOnBoardChanged;
        public bool CanAddNewUnitsToBoard => _board.FreeSpotsCount > 0;
        public bool CanRemoveUnitFromBoard => _board.OccupiedSpotsCount != 1;

        public Transform HeroPositionParent => _heroPlacement.HeroPosition;

        [SerializeField] private Board _board;
        [SerializeField] private ArmyTopInfo _armyTopInfo;
        [SerializeField] private GroupUnit[] _groupUnits;
        [SerializeField] private BoardUnitsPersistentHolder _boardUnitsPersistentHolder;
        [SerializeField] private Camera _canvasCamera;
        [SerializeField] private HeroPlacement _heroPlacement;
        [SerializeField] private int _maxLevel = 5;

        private IUnitHolder _firstSelectedUnitHolder, _lastSelectedUnitHolder;
        private bool _willBeUpgraded;
        private bool _isActive;
        private bool _isBoardUpdating;

        public void Init(int width, int height)
        {
            _board.Init(width, height);

            _board.EnteredSlotForUpgrade += HandleEnteredNewSlotForUpgrade;
            _board.ChangedSelectedSlot += UpdateSelectedSlot;
        }

        public Vector3 FreeSpotPositionInWorld()
        {
            Vector2 positionOnBoard = _board.FreeSpotPosition;
            return _board.BattleSlots.First(n => n.Position == positionOnBoard).transform.position;
            //return new Vector3(positionOnBoard.x, 0, positionOnBoard.y);
        }

        public void AddUnitToFreeSpot(string name, int level)
        {
            if (_board.FreeSpotsCount == 0)
            {
                Debug.Log("No free spots on board");
                return;
            }

            var spawnedUnit = Instantiate(_groupUnits[level - 1]);
            IBoardUnit unitInterface = spawnedUnit.GetComponent<IBoardUnit>();
            unitInterface.Init(name, level, _board.FreeSpotPosition);
            _board.AddToFreeSpot(unitInterface);
            UnitsOnBoardChanged?.Invoke();
        }

        public void RemoveUnitFromBoard(IBoardUnit boardUnit)
        {
            _board.RemoveUnitFromBoard(boardUnit);
        }

        public void Load(BoardUnitData[] boardUnits)
        {
            List<IBoardUnit> spawnedBoard = new List<IBoardUnit>();
            foreach (var unitData in boardUnits)
            {
                var spawnedUnit = Instantiate(_groupUnits[unitData.Level - 1]);
                IBoardUnit unitInterface = spawnedUnit.GetComponent<IBoardUnit>();
                unitInterface.Init(unitData.Name, unitData.Level, unitData.Position);
                spawnedBoard.Add(unitInterface);
            }
            _board.Load(spawnedBoard.ToArray());
        }

        public void StartBoardUpdate(IBoardUnit boardUnit)
        {
            _isBoardUpdating = true;

            IUnitHolder unit = _board.GetBattleSlotByPosition(boardUnit.Position);
            _firstSelectedUnitHolder = unit;
            if (unit.Unit != null)
            {
                var unitName = Lean.Localization.LeanLocalization.GetTranslation(unit.Unit.Name).Data.ToString();
                _armyTopInfo.SetUp(unitName, unit.Unit.Level);
                _board.FindNextUpgrade(unit.Unit, _maxLevel);
            }
        }

        public void BoardUpdate()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = _canvasCamera.transform.position.z * -1f;
            _armyTopInfo.Move(_canvasCamera.ScreenToWorldPoint(mousePosition));
            _board.HiglightActiveSlot();
        }

        public void StopBoardUpdate(bool movedToBarracks, out bool willBeUpgraded)
        {
            willBeUpgraded = false;
            _armyTopInfo.ToggleVisibility(false);
            _board.StopBoardUpdate();
            _isBoardUpdating = false;
            if (movedToBarracks == false)
                MoveToNewSlot(_firstSelectedUnitHolder, _lastSelectedUnitHolder, out willBeUpgraded);
                
        }

        public void ToggleArmyTopInfoVisibility(bool visible)
        {
            if (_isBoardUpdating == false)
                return;
            _armyTopInfo.ToggleVisibility(visible);
        }

        public void MoveToNewSlot(IUnitHolder oldSlot, IUnitHolder newSlot, out bool willBeUpgraded)
        {
            willBeUpgraded = false;

            if (oldSlot == null || newSlot == null)
                return;
            if (oldSlot.Unit == null && newSlot.Unit == null)
                return;
            if (oldSlot == newSlot) 
                return;

            if (_willBeUpgraded)
            {
                var newSlotUnitData = new BoardUnitData(newSlot.Unit.Name, newSlot.Unit.Level, newSlot.Position);
                var oldSlotUnitData = new BoardUnitData(oldSlot.Unit.Name, oldSlot.Unit.Level, oldSlot.Position);
                _boardUnitsPersistentHolder.HandleUpgrade(newSlotUnitData, oldSlotUnitData);
                newSlot.SetNewUnit(oldSlot.Unit);
                oldSlot.SetNewUnit(null);
                willBeUpgraded = true;
                return;
            }
            if (newSlot.Unit == null)
            {
                IBoardUnit newTemp = newSlot.Unit;
                _boardUnitsPersistentHolder.RemoveUnit(new BoardUnitData(oldSlot.Unit.Name, oldSlot.Unit.Level, oldSlot.Unit.Position));
                newSlot.SetNewUnit(oldSlot.Unit);
                _boardUnitsPersistentHolder.AddUnit(new BoardUnitData(newSlot.Unit.Name, newSlot.Unit.Level, newSlot.Unit.Position));
                oldSlot.SetNewUnit(newTemp);
            }
            else
            {
                var newSlotUnitData = new BoardUnitData(newSlot.Unit.Name, newSlot.Unit.Level, newSlot.Position);
                var oldSlotUnitData = new BoardUnitData(oldSlot.Unit.Name, oldSlot.Unit.Level, oldSlot.Position);
                _boardUnitsPersistentHolder.HandleUnitsChangedPlaces(newSlotUnitData, oldSlotUnitData);
                IBoardUnit newTemp = newSlot.Unit;
                newSlot.SetNewUnit(oldSlot.Unit);
                oldSlot.SetNewUnit(newTemp);
            }
        }

        public void SetUnitInLastSelectedSlot(IBoardUnit unit)
        {
            _lastSelectedUnitHolder.SetNewUnit(unit);
            BattleSlot battleSlot = _board.GetBattleSlotByUnitHolder(_lastSelectedUnitHolder);
            unit.Move(battleSlot.Position);
            _board.ActivateUpgradeEffect(battleSlot);
        }

        public List<GameObject> GetAllUnitsOnBoard()
        {
            return _board.GetAllUnitsOnBoard();
        }

        public List<IBoardUnit> GetAllBoardUnits()
        {
            return _board.GetAllBoardUnits();
        }

        public void DeactivateBoardUnitBehaviour()
        {
            var units = _board.GetAllBoardUnits();
            foreach (var unit in units)
            {
                unit.StopBeingBoardUnit();
            }
        }

        public void DisableBoard()
        {
            _board.ResetBattleSlots();
            _board.gameObject.SetActive(false);
            _lastSelectedUnitHolder = null;
            _firstSelectedUnitHolder = null;
            //Destroy(_board.gameObject);
        }

        public void EnableBoard()
        {
            _board.gameObject.SetActive(true);
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        private void Update()
        {
            if (_isActive)
            {
                BoardUpdate();
            }
        }

        private void UpdateSelectedSlot(IUnitHolder unitHolder)
        {
            _lastSelectedUnitHolder = unitHolder;
        }

        private void HandleEnteredNewSlotForUpgrade(bool isNewSlotAvailableForUpgrade)
        {
            _armyTopInfo.ToggleUpgradeArrow(isNewSlotAvailableForUpgrade);
            _willBeUpgraded = isNewSlotAvailableForUpgrade;
        }
    }
}
