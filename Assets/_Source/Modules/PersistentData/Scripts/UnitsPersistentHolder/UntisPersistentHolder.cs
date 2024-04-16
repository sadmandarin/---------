using System;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    public class UntisPersistentHolder<T> : ScriptableObject
    {
        public Action UnitsChanged;
        [field: SerializeField] public List<T> Units { get; private set; } = new List<T>();

        public void RemoveUnit(T unit)
        {
            Units.Remove(unit);
            UnitsChanged?.Invoke();
        }

        public void AddUnit(T unit)
        {
            Units.Add(unit);
            UnitsChanged?.Invoke();
        }

        public void LoadUnits(List<T> units)
        {
            Units = units;
        }
    }
}
