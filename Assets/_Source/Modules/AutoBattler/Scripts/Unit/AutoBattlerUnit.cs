using System;
using System.Collections.Generic;
using UnityEngine;
using UnitsData;

namespace AutoBattler
{
    [RequireComponent(typeof(Health), typeof(PathFinder), typeof(BoxCollider))]
    [RequireComponent(typeof(BattleReportView), typeof(EnemyAppearanceMaker))]
    internal class AutoBattlerUnit : MonoBehaviour, IDamageable
    {
        internal Action<AutoBattlerUnit> UnitDied;
        internal Action<AttackEventData> AttackPerformed;
        internal Action<HealthHealedEventData> HealthHealed;
        internal AutoBattlerUnit Target => _target;
        internal Faction Faction => _faction;
        internal float AttackStat => _attacker.AttackStat;
        internal float DefenseStat => _health.DefenseStat;
        internal int RewardForKilling => _rewardForKilling.Reward;
        internal int LevelOfUnit { get => _levelOfUnit; private set => _levelOfUnit = value; }

        [SerializeField] private Health _health;
        [SerializeField] private Faction _faction;
        [SerializeField] private Attacker _attacker;
        [SerializeField] private PathFinder _pathFinder;
        [SerializeField] private TargetFinder _targetFinder;
        [SerializeField] private UnitAnimator _unitAnimator;

        [field: SerializeField] internal bool IsHero { get; private set; }
        [SerializeField] private PassiveHeroBaseSkill _passiveHeroSkill;
        [SerializeField] private ActiveHeroBaseSkill _activeHeroSkill;

        [field: SerializeField] internal bool HasAutoAttackSkill { get; private set; }
        [SerializeField] private BaseAutoAttackSkill _autoAttackSkill;

        [SerializeField] private UnitStatsData _unitData;
        [SerializeField] private ModifiersHolder _modifiersHolder;
        [SerializeField] private BattleReportView _battleReportView;
        [SerializeField] private EnemyAppearanceMaker _enemyAppearanceMaker;
        [SerializeField] private RewardForKilling _rewardForKilling;

        private AutoBattlerUnit _target;
        private int _levelOfUnit = 0;
        private UnitStats _unitStats;
        private bool _makeDamageZero;

        internal void SetLevel(int level)
        {
            _levelOfUnit = level - 1;
        }

        internal void InitUnitStats()
        {
            if (_levelOfUnit == - 1)
            {
                Debug.LogError("Setting unit stats before setting level");
                return;
            }
            //Inititializing unit stats
            _unitStats = _unitData.StatsLevels[_levelOfUnit];
            _health.Init(_unitStats.Health, _unitStats.Defense, IsHero);
            _attacker.SetStats(_makeDamageZero ? 0 : _unitStats.Damage, _unitStats.AttackSpeed, _unitStats.HitRadius);
            _pathFinder.SetMovementSpeed(_unitStats.MovementSpeed);

            if (HasAutoAttackSkill) _autoAttackSkill.InitSkill(_levelOfUnit);

            _health.HealthReachedZero -= HealthReachedZeroHandler;
            _health.HealthReachedZero += HealthReachedZeroHandler;
            _modifiersHolder.ModifiersChanged -= CalculateStats;
            _modifiersHolder.ModifiersChanged += CalculateStats;
        }

        internal void MakeDamageZero()
        {
            _makeDamageZero = true;
        }

        internal void InitHeroSkills(ActiveSkillButton skillButton)
        {
            if (IsHero == false)
                return;
            if (_activeHeroSkill != null) _activeHeroSkill.Init(_levelOfUnit, _faction, skillButton);
            if (_passiveHeroSkill != null) _passiveHeroSkill.InitAndActivate(_levelOfUnit, _faction);
        }

        internal void InitUnitTargets(List<Transform> enemies)
        {
            _target = _targetFinder.FindTarget(enemies).GetComponent<AutoBattlerUnit>();
            _pathFinder.SetNewTarget(_target.transform);
            _unitAnimator.AnimateRun();
        }

        internal void InitBattleReportID(int uniqueID)
        {
            _battleReportView.Init(_unitData.name, _levelOfUnit, _faction, uniqueID);
            _attacker.SetBattleReportID(_battleReportView.ID);
        }

        internal void FindNewTarget(List<Transform> enemies)
        {
            _target = _targetFinder.FindTarget(enemies).GetComponent<AutoBattlerUnit>();
            _pathFinder.SetNewTarget(_target.transform);
            _unitAnimator.ResetAttack();
        }

        internal void ApplyModifier(float duration, float damageModifier = 1, float defenseModifier = 1,
            float attackSpeedModifier = 1, float movementSpeedModifier = 1)
        {
            _modifiersHolder.AddModifier(duration, damageModifier, defenseModifier, attackSpeedModifier, movementSpeedModifier);
        }

        public void TakePhysicalHit(BattleReportID attackingUnitID, float attackStat)
        {
            _health.TakeDamage(attackStat, out float effectiveDamage, out float damageBlocked);
            var attackEvent = new AttackEventData(attackingUnitID, _battleReportView.ID, effectiveDamage, damageBlocked);
            AttackPerformed?.Invoke(attackEvent);
        }

        public void TakeMagicHit(BattleReportID attackingUnitID, float attackStat)
        {
            _health.TakeDamage(attackStat, out float effectiveDamage, out float damageBlocked, ignoreDefense: true);
            var attackEvent = new AttackEventData(attackingUnitID, _battleReportView.ID, effectiveDamage, damageBlocked);
            AttackPerformed?.Invoke(attackEvent);
        }

        internal void CelebrateVictory()
        {
            _unitAnimator.AnimateVictory();
        }

        internal void ApplyHealthModifier(float healthModifier)
        {
            _health.ApplyHealthModifier(healthModifier);
        }

        internal void AddInvincibleBarrier(int numberOfHits, Action onBarrierBroken)
        {
            _health.AddInvincibleBarrier(numberOfHits, onBarrierBroken);
        }

        internal void HealPercentOfTotalHealth(BattleReportID healingUnitID, float percentToHeal)
        {
            _health.HealPercentOfTotalHealth(percentToHeal, out float amountHealed);
            var HealthHealedEventData = new HealthHealedEventData(healingUnitID, amountHealed);
            HealthHealed?.Invoke(HealthHealedEventData);
        }

        internal void Heal(BattleReportID healingUnitID, float amountToHeal)
        {
            _health.Heal(amountToHeal, out float amountHealed);
            var HealthHealedEventData = new HealthHealedEventData(healingUnitID, amountHealed);
            HealthHealed?.Invoke(HealthHealedEventData);
        }

        internal void SetNewTargetFinder(TargetFinder targetFinder)
        {
            _targetFinder = targetFinder;
        }

        internal void AddShield(float shieldAmount, GameObject shieldEffect)
        {
            _health.AddShield(shieldAmount, shieldEffect);
        }

        internal void MakeUnitAnEnemy()
        {
            _faction = Faction.Enemy;
            _enemyAppearanceMaker.ChangeAppearanceToEnemy();
        }

        internal void SetRewardForKilling(int value)
        {
            _rewardForKilling.SetValue(value);
        }

        private void CalculateStats()
        {
            _attacker.SetStats(_unitStats.Damage * _modifiersHolder.TotalAttackModifer,
                               _unitStats.AttackSpeed * _modifiersHolder.TotalAttackSpeedModifier,
                               _unitStats.HitRadius);
            _pathFinder.SetMovementSpeed(_unitStats.MovementSpeed * _modifiersHolder.TotalMovementSpeedModifier);
            _health.SetDefense(_unitStats.Defense * _modifiersHolder.TotalDefenseMOdifier);
        }

        private void HealthReachedZeroHandler()
        {
            UnitDied?.Invoke(this);
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_pathFinder.TargetInReach && _attacker.CanAttack)
            {
                if (HasAutoAttackSkill && _autoAttackSkill.CanActivateSkill(_attacker.TimesAttacked))
                {
                    _autoAttackSkill.ActivateSkill(_target, _levelOfUnit);
                    _attacker.StartAttackDelay();
                    _attacker.ResetNumberOfAttacks();
                }
                else
                {
                    _attacker.Attack(_target);
                    _attacker.StartAttackDelay();
                }
            }
        }
    }
}
