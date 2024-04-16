using System;
using UnityEngine;

namespace AutoBattler
{
    internal class PaladinSkill : BaseAutoAttackSkill
    {
        [SerializeField] private PaladinSkillLevelsData[] _skillLevels;
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private float _range;
        [SerializeField] private GameObject _shieldEffect;

        private int _skillLevel;
        private Faction _enemyFaction;

        internal override void ActivateSkill(AutoBattlerUnit unit, int levelOfSkill)
        {
            _skillLevel = levelOfSkill;
            _enemyFaction = unit.Faction;
            _unitAnimator.AnimateSkill(OnSkillAnimationEnded);
        }

        private void OnSkillAnimationEnded()
        {
            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_range, transform.position, AddShieldsToUnits);
        }

        private void AddShieldsToUnits(AutoBattlerUnit unit)
        {
            if (unit.Faction != _enemyFaction)
            {
                unit.AddShield(_skillLevels[_skillLevel].ShieldAmount, _shieldEffect);
            }
        }
    }

    [Serializable]
    internal struct PaladinSkillLevelsData
    {
        public float ShieldAmount;
    }
}
