using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Experience/CollectedUnits")]
    public class CollectedUnitsCollection : PersistentCollection<CollectedUnitData>
    {
        internal bool TryAddUnit(string name, int level)
        {
            if (CollectionValue.Contains(new CollectedUnitData(name, level)))
                return false;
            else
            {
                CollectionValue.Add(new CollectedUnitData(name, level));
                CollectionChanged?.Invoke();
                return true;
            }
        }
    }


    [Serializable]
    public struct CollectedUnitData
    {
        public string Name;
        public int Level;

        public CollectedUnitData(string name, int level)
        {
            Name = name;
            Level = level;
        }
    }
}
