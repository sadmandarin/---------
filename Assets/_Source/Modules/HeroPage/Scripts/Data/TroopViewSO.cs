using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitsData;

namespace HeroPage
{
    [CreateAssetMenu(menuName = "HeroPageView/Troop")]
    internal class TroopViewSO : ScriptableObject
    {
        [field: SerializeField] internal UnitViewSO View { get; private set; }
        [field: SerializeField] internal int UnlockedAtLevel => _purchaseData.LevelRequirement;
        [field: SerializeField] internal UnitStatsData Stats { get; private set; }
        [field: SerializeField] internal bool IsSingleCount { get; private set; }
        [field: SerializeField] internal bool IsMagicAttacker { get; private set; }
        [field: SerializeField] internal TypeOfAttack AttackType { get; private set; }
        [field: SerializeField] internal TypeofDistanceOfAttack AttackDistance { get; private set; }
        [field: SerializeField] internal TypeOfTargetSelection TargetSelection { get; private set; }
        [field: SerializeField] internal TypeOfDamageShape DamageShape { get; private set; }
        [field: SerializeField] internal bool HasSkill { get; private set; }
        [field: SerializeField] internal TroopSkillView SkillView { get; private set; }

        [SerializeField] private UnitPurchaseDataSO _purchaseData;

        internal string Description => View.Name.ToString() + "Description";
    }

    

    internal enum TypeOfAttack
    {
        Melee,
        Ranged
    }

    internal enum TypeofDistanceOfAttack
    {
        Short,
        Medium,
        Infinity
    }

    internal enum TypeOfDamageShape
    {
        Single,
        Round,
        Square
    }

    internal enum TypeOfTargetSelection
    {
        ClosestTarget,
        RandomTarget
    }
}