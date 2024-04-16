using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class AutoBattlerUnitsManager : MonoBehaviour
    {
        internal Action<bool> NoMoreTargetsLeft;
        internal Action<AutoBattlerUnit> EnemyUnitDied;
        internal List<AutoBattlerUnit> Units => _units;
        internal int UnitsRemainingAlive => _units.Count;

        [SerializeField] private List<AutoBattlerUnit> _units;
        [SerializeField] private Transform _playerUnitParent;
        [SerializeField] private StrengthBalanceInfo _strengthBalanceInfo;
        [SerializeField] private UndeadUnitSpawner _undeadUnitSpawner;

        internal void AddEnemyUnits(List<AutoBattlerUnit> units)
        {
            foreach (var unit in units)
            {
                _units.Add(unit);
            }
        }

        internal void ClearEnemyUnits()
        {
            foreach (var unit in _units)
            {
                if (unit.Faction == Faction.Enemy)
                    Destroy(unit.gameObject);
            }
            _units.RemoveAll(n => n.Faction == Faction.Enemy);
            
        }

        internal void AddPlayerUnitsWithoutRepeating(List<GameObject> playerUnits, int level)
        {
            _units.RemoveAll(n => n == null);
            foreach (var unit in playerUnits)
            {
                if (unit.TryGetComponent(out AutoBattlerUnit autoBattlerUnit))
                {
                    if (_units.Contains(autoBattlerUnit))
                        continue;
                    autoBattlerUnit.SetLevel(level);
                    _units.Add(autoBattlerUnit);
                }
            }
            UpdateStrengthBalance();
        }

        internal void AddPlayerHero(AutoBattlerUnit playerHero)
        {
            if (_units.Contains(playerHero))
                return;
            _units.Add(playerHero);
        }

        internal void MovePlayerUnitsToAnotherParent()
        {
            var playerUnitsInList = _units.Where(n => n.Faction == Faction.Player).ToList();
            foreach (var unit in playerUnitsInList)
            {
                unit.transform.SetParent(_playerUnitParent);
            }
        }

        internal void UpdateUnitsStats()
        {
            var playerUnits = _units.Where(n => n.Faction == Faction.Player).ToList();
            var playerHero = playerUnits.FirstOrDefault(n => n.IsHero);
            var enemyUnits = _units.Where(n => n.Faction == Faction.Enemy).ToList();
            var enemyHero = enemyUnits.FirstOrDefault(n => n.IsHero);

            foreach (var unit in playerUnits)
            {
                if (unit.IsHero) continue;
                unit.InitUnitStats();
            }

            foreach (var unit in enemyUnits)
            {
                if (unit.IsHero) continue;
                unit.InitUnitStats();
            }

            if (playerHero != null) playerHero.InitUnitStats();
            if (enemyHero != null) enemyHero.InitUnitStats();
        }

        internal void UnitDiedHandler(AutoBattlerUnit unitThatDied)
        {
            _units.Remove(unitThatDied);

            if (unitThatDied.Faction == Faction.Enemy)
            {
                EnemyUnitDied?.Invoke(unitThatDied);
            }

            if (unitThatDied.TryGetComponent(out RessurectableUndeadSoldierTag undeadSoldier))
            {
                _undeadUnitSpawner.SpawnUndead(unitThatDied.transform.position, unitThatDied.LevelOfUnit, unitThatDied.Faction);
            }
            

            var unitsThatHadThisUnitAsTarget = _units.Where(n => n.Target == unitThatDied).ToList();

            if (unitsThatHadThisUnitAsTarget.Count == 0)
                return;

            

            var possibleTargets = _units.Where(n => n.Faction == unitThatDied.Faction).ToList();

            if (possibleTargets.Count == 0)
            {
                foreach (var unit in _units)
                {
                    unit.CelebrateVictory();
                }
                if (_units[0].Faction == Faction.Player)
                    NoMoreTargetsLeft?.Invoke(true);
                else
                    NoMoreTargetsLeft?.Invoke(false);
                return;
            }

            foreach (var unit in unitsThatHadThisUnitAsTarget)
            {
                unit.FindNewTarget(possibleTargets.Select(n => n.transform).ToList());
            }
        }

        internal void InitUnitsForBattle()
        {
            ActivateUnitsColliders();
            InitUnitTargets();
            AssignUnitDiedHandler();
        }

        private void ActivateUnitsColliders()
        {
            foreach (var unit in _units)
            {
                if (unit.TryGetComponent(out NavMeshAgent agent))
                    agent.enabled = true;
                if (unit.TryGetComponent(out BoxCollider collider))
                    collider.enabled = true;
                if (unit.TryGetComponent(out NavMeshObstacle obstacle))
                    obstacle.enabled = true;
            }
        }

        private void AssignUnitDiedHandler()
        {
            foreach (var unit in _units)
            {
                unit.UnitDied -= UnitDiedHandler;
                unit.UnitDied += UnitDiedHandler;
                unit.UnitDied -= UpdateStrengthBalance;
                unit.UnitDied += UpdateStrengthBalance;
            }
        }

        private void UpdateStrengthBalance(AutoBattlerUnit unit = null)
        {
            if (_strengthBalanceInfo == null)
                return;
            _strengthBalanceInfo.CalculateAndSetStats(Units);
        }

        internal void InitUnitTargets()
        {
            var playerUnits = _units.Where(n => n.Faction == Faction.Player).ToList();
            var enemyUnits = _units.Where(n => n.Faction == Faction.Enemy).ToList();

            foreach (var unit in playerUnits)
            {
                unit.InitUnitTargets(enemyUnits.Select(n => n.transform).ToList());
            }

            foreach (var unit in enemyUnits)
            {
                unit.InitUnitTargets(playerUnits.Select(n => n.transform).ToList());
            }
        }

        internal void ClearUnits()
        {
            foreach (var unit in _units)
            {
                Destroy(unit.gameObject);
            }
            _units.Clear();
        }
    }
}
