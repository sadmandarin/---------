using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class TreeOfLifePassiveSkill : PassiveHeroBaseSkill
    {
        [SerializeField] private float _range = 2;
        [SerializeField] private float _timeToRegenerate = 1;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private TreeOfLifePassiveSkillLevel[] _levels;

        private Faction _selfFaction;
        private TreeOfLifePassiveSkillLevel _selectedLevelSkill;
        private BattleReportID _battleReportID;
        private bool _battleReportIDSet = false;

        internal override void ActivateSkill(Faction faction, int level, float range, Vector3 startPosition)
        {
            _selectedLevelSkill = _levels[level];
            _selfFaction = faction;
            StartCoroutine(HealOverTime());
        }

        private IEnumerator HealOverTime()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(_timeToRegenerate);
                SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_range, _heroPosition.position, HealUnits);
            }
            
        }

        private void HealUnits(AutoBattlerUnit unit)
        {
            if (unit.Faction == _selfFaction)
            {
                if (_battleReportIDSet == false)
                {
                    _battleReportID = transform.GetComponent<BattleReportView>().ID;
                    _battleReportIDSet = true;
                }

                unit.HealPercentOfTotalHealth(_battleReportID, _selectedLevelSkill.PercentToHeal);
            }
        }
    }

    [Serializable]
    internal struct TreeOfLifePassiveSkillLevel
    {
        public float PercentToHeal;
    }
}
