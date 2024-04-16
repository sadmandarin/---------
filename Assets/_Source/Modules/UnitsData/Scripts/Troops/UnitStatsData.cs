using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu]
    public class UnitStatsData : ScriptableObject
    {
        [field: SerializeField] public UnitStats[] StatsLevels;
    }

    [Serializable]
    public struct UnitStats
    {
        public float Health;
        public float Defense;
        public float Damage;
        public float HitRadius;
        public float AttackSpeed;
        public float MovementSpeed;
    }
}
