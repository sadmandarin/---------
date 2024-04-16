using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class JungleHunterPassiveSkill : BaseAutoAttackSkill
    {
        [SerializeField] private Attacker _attacker;
        [SerializeField] private float _debuffTime = 2f;

        internal override void ActivateSkill(AutoBattlerUnit unit, int levelOfSkill)
        {
            unit.ApplyModifier(_debuffTime, defenseModifier: 0);
            _attacker.Attack(unit);
        }
    }
}
