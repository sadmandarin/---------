using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public abstract class BaseAutoAttackSkill : MonoBehaviour
    {
        protected BattleReportID BattleReportID => GetComponent<BattleReportView>().ID;

        [SerializeField] private int[] _timesToActivate;

        protected int TimesOfAttacksToActivate
        {
            get
            {
                return _timesToActivate[_levelOfSkill >= _timesToActivate.Length ? _timesToActivate.Length - 1 : _levelOfSkill];
            }
        }

        private int _levelOfSkill;

        internal void InitSkill(int levelOfSkill)
        {
            _levelOfSkill = levelOfSkill;
        }

        internal bool CanActivateSkill(int currentNumberOfAttacks)
        {
            return TimesOfAttacksToActivate <= currentNumberOfAttacks && ActivationRequirementFullfilled();
        }

        internal abstract void ActivateSkill(AutoBattlerUnit unit, int levelOfSkill);

        internal virtual bool ActivationRequirementFullfilled()
        {
            return true;
        }
    }
}
