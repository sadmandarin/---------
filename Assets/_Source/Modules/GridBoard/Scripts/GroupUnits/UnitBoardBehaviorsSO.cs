using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnitsData;

namespace GridBoard
{
    [CreateAssetMenu]
    internal class UnitBoardBehaviorsSO : ScriptableObject
    {
        [field:SerializeField] private List<UnitBoardDataSO> Prefabs;
        
        internal GameObject GetPrefabByName(string name) => Prefabs.FirstOrDefault(n => n.Name.ToString() == name).Prefab;
        internal bool IsUnitSingleCount(string name) => Prefabs.FirstOrDefault(n => n.Name.ToString() == name).IsSingleCount;
    }

    [Serializable]
    internal struct UnitPrefabData
    {
        public string Name;
        public bool IsSingleCount;
        public GameObject Prefab;
    }
}
