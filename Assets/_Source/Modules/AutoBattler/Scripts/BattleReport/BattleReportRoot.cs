using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    public class BattleReportRoot : MonoBehaviour
    {
        [SerializeField] private List<BattleReportUnit> _battleReportUnits = new List<BattleReportUnit>();

        private int _lastId, _level;

        public int Level { get => _level; private set => _level = value; }

        internal void Init(List<AutoBattlerUnit> units, int level)
        {
            for (int i = 0; i < units.Count; i++)
            {
                AutoBattlerUnit unit = units[i];
                AddUnitToBattleReport(unit);
            }
            _level = level;
        }

        internal void AddUnitToBattleReport(AutoBattlerUnit unit )
        {
            unit.InitBattleReportID(_lastId);
            _lastId += 1;
            unit.AttackPerformed += HandleAttackEvent;
            unit.HealthHealed += HandleHealthHealed;
            var view = unit.GetComponent<BattleReportView>();
            _battleReportUnits.Add(new BattleReportUnit(view.Name, view.Level, view.Faction, view.UnitIcon, view.UniqueID, unit.IsHero));
        }

        internal void ResetData()
        {
            _battleReportUnits.Clear();
            _lastId = 0;
        }

        private void HandleHealthHealed(HealthHealedEventData data)
        {
            if (data.HealthHealed == 0)
                return;

            var unitThatHealed = _battleReportUnits.FirstOrDefault(n => n.ID == data.UnitThatHealed);

            if (unitThatHealed == null)
            {
                Debug.LogError("Couldn't find unit that healed");
                return;
            }
            unitThatHealed.IncreaseHealthHealed(data.HealthHealed);
        }

        internal void HandleAttackEvent(AttackEventData attackData)
        {
            if (attackData.BlockedDamage == 0 && attackData.EffectiveDamage == 0)
                return;

            var unitThatWasAttacked = _battleReportUnits.FirstOrDefault(n => n.ID == attackData.UnitThatWasAttacked);
            var unitThatAttacked = _battleReportUnits.FirstOrDefault(n => n.ID == attackData.UnitThatAttacked);

            if (unitThatAttacked == null || unitThatWasAttacked == null)
            {
                Debug.LogError("Couldn't find unit in battle report");
                return;
            }

            unitThatAttacked.IncreaseDamageDealt(attackData.EffectiveDamage);
            unitThatWasAttacked.IncreaseDamageTaken(attackData.EffectiveDamage);
            unitThatWasAttacked.IncreaseDamageBlocked(attackData.BlockedDamage);
        }

        internal BattleReportStatsItem GetFactionStats(Faction faction)
        {
            var totalDamageDealt = _battleReportUnits.Where(n => n.ID.Faction == faction).Sum(n => n.DamageDealt);
            var totalDamageTaken = _battleReportUnits.Where(n => n.ID.Faction == faction).Sum(n => n.DamageTaken);
            var totalDamageBlocked = _battleReportUnits.Where(n => n.ID.Faction == faction).Sum(n => n.DamageBlocked);
            var totalHealed = _battleReportUnits.Where(n => n.ID.Faction == faction).Sum(n => n.HealthHealed);

            return new BattleReportStatsItem(totalDamageDealt, totalDamageTaken, totalDamageBlocked, totalHealed);
        }

        internal BattleReportUnit HeroReport(Faction faction) => _battleReportUnits.FirstOrDefault(n => n.IsHero && n.ID.Faction == faction);

        internal List<BattleReportUnit> Get5BestUnits(Faction factionOfUnits, BattleReportStat stat)
        {
            var unitsOfTargetFaction = _battleReportUnits.Where(n => n.ID.Faction == factionOfUnits).ToList();
            var combinedTroops = CombineCommonTroops(unitsOfTargetFaction);
            switch (stat)
            {
                case BattleReportStat.DamageDealt:
                    return combinedTroops.OrderByDescending(n => n.DamageDealt).Take(5).ToList();
                case BattleReportStat.DamageRecieved:
                    return combinedTroops.OrderByDescending(n => n.DamageTaken).Take(5).ToList();
                case BattleReportStat.DamageBlocked:
                    return combinedTroops.OrderByDescending(n => n.DamageBlocked).Take(5).ToList();
                case BattleReportStat.Healed:
                    return combinedTroops.OrderByDescending(n => n.HealthHealed).Take(5).ToList();
            }

            return null;
        }

        private List<BattleReportUnit> CombineCommonTroops(List<BattleReportUnit> units)
        {
            List<BattleReportUnit> combinedTroops = new List<BattleReportUnit>();
            for (int i = 0; i < units.Count; i++)
            {
                BattleReportUnit unit = units[i];
                if (combinedTroops.FirstOrDefault(n => n.ID.Level == unit.ID.Level && n.ID.Name == unit.ID.Name) != null)
                    continue;

                if (unit.IsHero)
                {
                    combinedTroops.Add(unit);
                    continue;
                }

                BattleReportUnit combinedUnit = new BattleReportUnit(unit.ID.Name, unit.ID.Level, unit.ID.Faction, unit.Icon, i, unit.IsHero);
                
                for (int j = 0; j < units.Count; j++)
                {
                    if (combinedUnit.ID.Level == units[j].ID.Level && combinedUnit.ID.Name == units[j].ID.Name)
                    {
                        combinedUnit.IncreaseDamageDealt(units[j].DamageDealt);
                        combinedUnit.IncreaseDamageTaken(units[j].DamageTaken);
                        combinedUnit.IncreaseDamageBlocked(units[j].DamageBlocked);
                        combinedUnit.IncreaseHealthHealed(units[j].HealthHealed);
                        combinedUnit.IncreaseQuantityOfTroops();
                    }
                }
                combinedTroops.Add(combinedUnit);
            }
            return combinedTroops;
        }

    }

    internal struct BattleReportStatsItem
    {
        public float DamageDealt;
        public float DamageTaken;
        public float DamageBlocked;
        public float Healed;

        public BattleReportStatsItem(float damageDealt, float damageRecieved, float damageBlocked, float healed)
        {
            DamageDealt = damageDealt;
            DamageTaken = damageRecieved;
            DamageBlocked = damageBlocked;
            Healed = healed;
        }
    }

    internal enum BattleReportStat
    {
        DamageDealt,
        DamageRecieved,
        DamageBlocked,
        Healed
    }
}
