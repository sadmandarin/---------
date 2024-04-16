using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class BattleReportRankPage : MonoBehaviour
    {
        [SerializeField] private List<BattleReportRankItem> _playerRankItems = new List<BattleReportRankItem>();
        [SerializeField] private List<BattleReportRankItem> _enemyRankItems = new List<BattleReportRankItem>();
        [SerializeField] private BattleReportRankPageTabGroup _tabGroup;

        private BattleReportRoot _reportRoot;
        
        private const int NumberOfUnitsForRanking = 5;

        internal void Init(BattleReportRoot root)
        {
            _reportRoot = root;
            _tabGroup.ChangeTab(BattleReportStat.DamageDealt);
        }

        internal void ChangeTabs(BattleReportStat statTab)
        {
            FillPageWithData(_reportRoot.Get5BestUnits(Faction.Player, statTab), _reportRoot.Get5BestUnits(Faction.Enemy, statTab), statTab);
        }

        private void FillPageWithData(List<BattleReportUnit> playerUnits, List<BattleReportUnit> enemyUnits, BattleReportStat statToRank)
        {
            FillRankings(playerUnits, _playerRankItems, statToRank);
            FillRankings(enemyUnits, _enemyRankItems, statToRank);
        }

        private void FillRankings(List<BattleReportUnit> units, List<BattleReportRankItem> rankItems, BattleReportStat statToRank)
        {
            List<BattleReportRankItemData> unitsData = new List<BattleReportRankItemData>();

            foreach (var unit in units)
            {
                unitsData.Add(new BattleReportRankItemData(unit, statToRank));
            }

            var maxPlayerStat = unitsData[0].StatValue;

            for (int i = 0; i < NumberOfUnitsForRanking; i++)
            {
                rankItems[i].gameObject.SetActive(i < unitsData.Count);

                if (rankItems[i].isActiveAndEnabled == false)
                    continue;

                BattleReportRankItemData unitData = unitsData[i];
                var normalizedValue = maxPlayerStat == 0 ? 0 : Mathf.Clamp(unitData.StatValue / maxPlayerStat, 0, 1);
                rankItems[i].SetCommonData(unitData.Unit.Icon, unitData.StatValue, normalizedValue);

                if (unitData.Unit.IsHero)
                {
                    rankItems[i].SetHeroData(unitData.Unit.ID.Level);
                }
                else
                {
                    rankItems[i].SetTroopData(unitData.Unit.ID.Level, unitData.Unit.QuantityOfTroops);
                }
            }
        }
    }

    internal struct BattleReportRankItemData
    {
        public BattleReportUnit Unit;
        public float StatValue;

        public BattleReportRankItemData(BattleReportUnit unit, BattleReportStat stat)
        {
            Unit = unit;
            switch (stat)
            {
                case BattleReportStat.DamageDealt:
                    StatValue = unit.DamageDealt;
                    break;
                case BattleReportStat.DamageRecieved:
                    StatValue = unit.DamageTaken;
                    break;
                case BattleReportStat.DamageBlocked:
                    StatValue = unit.DamageBlocked;
                    break;
                case BattleReportStat.Healed:
                    StatValue = unit.HealthHealed;
                    break;
                default:
                    StatValue = unit.DamageDealt;
                    break;
            }
        }
    }
}