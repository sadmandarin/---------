using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace GridBoard
{
    public class BarracksRoot : MonoBehaviour
    {
        public Action<bool> UnitInsideBarracksChanged;
        public Action<IBarracksTroopCell> OnCellPressed;
        public Vector3 BarracksButtonPosition => _barracksButton.transform.position;

        [SerializeField] private BarracksCellsManager _unitsManager;
        [SerializeField] private BarracksPage _barracksPage;
        [SerializeField] private MovedToBarracksTrigger _barracksTrigger;
        [SerializeField] private BarracksTroopCell _barracksTroopCell;
        [SerializeField] private TroopsViewsSO _barracksTroopsViews;
        [SerializeField] private BarracksUnitsPersistentHolder _barracksUnitsPersistentHolder;
        [SerializeField] private BarracksButton _barracksButton;

        public void ClearCellsInBarracks()
        {
            _unitsManager.ClearCells();
        }

        public void LoadBarracksUnits(List<BarracksUnitData> units)
        {
            foreach (var unit in units)
            {
                InstantiateCellIntoBarracks(unit.Name, unit.Level);
            }
        }

        public bool TryAddToBarracks(GameObject objectToAdd)
        {
            if (objectToAdd.TryGetComponent(out BarracksUnit unit))
            {
                if (unit.InsideBarracks)
                {
                    InstantiateCellIntoBarracks(unit.Name, unit.Level);
                    _barracksUnitsPersistentHolder.AddUnit(new BarracksUnitData(unit.Name, unit.Level));
                    return true;
                }
            }

            return false;
        }

        public void AddToBarracksByName(string name, int level)
        {
            InstantiateCellIntoBarracks(name, level);
            _barracksUnitsPersistentHolder.AddUnit(new BarracksUnitData(name, level));
        }

        private void InstantiateCellIntoBarracks(string name, int level)
        {
            BarracksTroopCell cell = Instantiate(_barracksTroopCell);
            var barracksView = _barracksTroopsViews.GetTroopViewByName(name);
            cell.Init(barracksView.Icon, barracksView.rarity, level, name);
            cell.CellPressed += CellPressedHandler;
            _unitsManager.AddCell(cell);
        }

        public void RemoveUnitFromBarracks(IBarracksTroopCell troopCell)
        {
            _unitsManager.RemoveAndDestroyCell(troopCell);
            _barracksUnitsPersistentHolder.RemoveUnit(new BarracksUnitData(troopCell.Name, troopCell.Level)); 
        }

        private void OnEnable()
        {
            _unitsManager.NumberOfUnitsInBarracksChanged += _barracksPage.UpdateUnitNumbers;
            _barracksTrigger.SomeoneInsideBarracksStateChanged += SomeoneInsideBarracksStateChangedHandler;
        }

        private void OnDisable()
        {
            _unitsManager.NumberOfUnitsInBarracksChanged -= _barracksPage.UpdateUnitNumbers;
            _barracksTrigger.SomeoneInsideBarracksStateChanged -= SomeoneInsideBarracksStateChangedHandler;
        }

        private void SomeoneInsideBarracksStateChangedHandler(bool isInside)
        {
            UnitInsideBarracksChanged?.Invoke(isInside);
        }

        private void CellPressedHandler(IBarracksTroopCell barracksTroopCell)
        {
            OnCellPressed?.Invoke(barracksTroopCell);
        }
    }
    
}
