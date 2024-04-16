using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class BarracksCellsManager: MonoBehaviour
    {
        internal int NumberOfUnitsInsideBarracks => _barrackUnits.Count;
        internal Action<int> NumberOfUnitsInBarracksChanged;

        [SerializeField] private Transform _contentParent;

        private List<IBarracksTroopCell> _barrackUnits = new List<IBarracksTroopCell>();
        
        internal void ClearCells()
        {
            foreach (var cell in _barrackUnits)
            {
                cell.Destroy();
            }
            _barrackUnits.Clear();
        }

        internal void AddCell(BarracksTroopCell unit)
        {
            _barrackUnits.Add(unit);
            unit.transform.SetParent(_contentParent, false);
            unit.StopFollowingMouse();
            NumberOfUnitsInBarracksChanged?.Invoke(_barrackUnits.Count);
        }

        internal void RemoveAndDestroyCell(IBarracksTroopCell troopCell)
        {
            _barrackUnits.Remove(troopCell);
            troopCell.Destroy();
            NumberOfUnitsInBarracksChanged?.Invoke(_barrackUnits.Count);
        }
    }
}
