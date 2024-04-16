using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public abstract class ActiveHeroBaseSkill : MonoBehaviour
    {
        protected BattleReportID BattleReportID;

        [SerializeField] private ActiveSkillButton _skillButton;
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private float[] _cooldownsForEachLevel;
        [SerializeField] private UnitStopper _unitStopper;
        [SerializeField] private Sprite _heroHead;

        private int _skillLevel;
        private Faction _skillFaction;

        internal void Init(int level, Faction skillFaction, ActiveSkillButton skillButton)
        {
            _skillLevel = level;
            _skillFaction = skillFaction;
            
            var cooldown = _cooldownsForEachLevel[level >= _cooldownsForEachLevel.Length ? _cooldownsForEachLevel.Length - 1 : level];

            skillButton.Init(cooldown, ActivateSkill, _heroHead);
        }

        internal abstract void OnSkillActivated(int level, Faction selfFaction);

        private void ActivateSkill()
        {
            BattleReportID = GetComponent<BattleReportView>().ID;
            _unitStopper.StopMoving();
            _unitAnimator.AnimateSkill(() =>
            {
                OnSkillActivated(_skillLevel, _skillFaction);
                _unitStopper.ResumeMoving();
            });
        }
    }
}
