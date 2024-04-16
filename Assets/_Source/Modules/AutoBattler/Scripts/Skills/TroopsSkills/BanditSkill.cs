using AutoBattler;
using System;
using UnityEngine;

internal class BanditSkill : BaseAutoAttackSkill
{
    [SerializeField] private BanditSkillLevelData[] _levels;
    [SerializeField] private RangedAttacker _rangedUnitAttacker;
    [SerializeField] private MeleeAttacker _originalUnitAttacker;
    [SerializeField] private BattleReportView _battleReportView;
    [SerializeField] private PathFinder _pathFinder;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _agent;
    
    private int _timesSkillWasActivated;
    private int _numberOfRangedAttacks;

    internal override void ActivateSkill(AutoBattlerUnit unit, int levelOfSkill)
    {
        _numberOfRangedAttacks = _levels[levelOfSkill].NumberOfRangedAttack - 1;
        _rangedUnitAttacker.SetStats(_originalUnitAttacker.AttackStat, 1f, 0);
        _rangedUnitAttacker.SetBattleReportID(_battleReportView.ID);
        _rangedUnitAttacker.Attack(unit);
        _timesSkillWasActivated += 1;
        if (ActivationRequirementFullfilled() == false)
        {
            _pathFinder.ChangeAttackRange(1);
            _agent.stoppingDistance = 1;
        }
    }

    internal override bool ActivationRequirementFullfilled()
    {
        return _timesSkillWasActivated <= _numberOfRangedAttacks;
    }
}
[Serializable]
internal struct BanditSkillLevelData
{
    public int NumberOfRangedAttack;
}
