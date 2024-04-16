using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Units/BoardData")]
    public class UnitBoardDataSO : ScriptableObject
    {
        [field: SerializeField] public TroopName Name { get;private set; }
        [field:SerializeField] public bool IsSingleCount { get; private set; }
        [field:SerializeField] public GameObject Prefab { get; private set; }
    }
}
