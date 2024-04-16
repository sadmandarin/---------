using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal abstract class PassiveHeroBaseSkill : MonoBehaviour
    {
        [SerializeField] private float _rangeOfSkill = GlobalRangeSkill;
        [SerializeField] private Transform _passiveSkillsStartTransform;

        private const float GlobalRangeSkill = 100f;

        private bool _passiveSkillAlreadyActivated;

        internal void InitAndActivate(int level, Faction faction)
        {
            if (_passiveSkillAlreadyActivated)
                return;

            _passiveSkillAlreadyActivated = true;
            ActivateSkill(faction, level, _rangeOfSkill, _passiveSkillsStartTransform.position);
        }

        internal abstract void ActivateSkill(Faction faction, int level, float range, Vector3 startPosition);
    }
}
