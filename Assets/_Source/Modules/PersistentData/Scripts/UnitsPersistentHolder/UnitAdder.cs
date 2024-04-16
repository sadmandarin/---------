using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "UnitAdder")]
    public class UnitAdder : ScriptableObject
    {
        [SerializeField] private BarracksUnitsPersistentHolder _barracks;
        [SerializeField] private BoardUnitsPersistentHolder _board;

        public List<string> GetAllUnlockedUnits()
        {
            List<string> result = new List<string>();
            foreach (var unit in _barracks.Units) 
            {
                if (result.Contains(unit.Name) == false) 
                    result.Add(unit.Name);
            }
            foreach (var unit in _board.Units)
            {
                if (result.Contains(unit.Name) == false)
                    result.Add(unit.Name);
            }
            return result;
        }

        public void AddUnit(string name, int level)
        {
            if (_board.IsThereAFreeSpotOnBoard())
            {
                _board.AddUnitToFreeSpot(name, level);
            }
            else
            {
                _barracks.AddUnit(new BarracksUnitData(name, level));
            }
        }

        public bool HasUnit(string name)
        {
            if (_board.IsUnitOnBoard(name))
                return true;

            if (_barracks.IsUnitInBarracks(name))
                return true;

            return false;
        }
    }
}
