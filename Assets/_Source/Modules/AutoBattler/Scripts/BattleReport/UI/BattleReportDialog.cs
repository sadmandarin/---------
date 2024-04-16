using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public class BattleReportDialog : MonoBehaviour
    {
        [SerializeField] private BattleReportFactionItem _playerStats;
        [SerializeField] private BattleReportFactionItem _enemyStats;
        [SerializeField] private BattleReportRankPage _battleReportRankPage;
        [SerializeField] private BattlePlayerInfo _battlePlayerInfo;
        [SerializeField] private Canvas _canvas;
        
        private BattleReportRoot _battleReportRoot;

        internal void Init(BattleReportRoot battleReportRoot, bool hasWon)
        {
            _battleReportRoot = battleReportRoot;

            InitCamera();
            InitBattlePlayerInfo(hasWon, _battleReportRoot.Level);
            InitFactionStats(_battleReportRoot.GetFactionStats(Faction.Player), _battleReportRoot.GetFactionStats(Faction.Enemy));
            InitHeroStats(_battleReportRoot.HeroReport(Faction.Player), _battleReportRoot.HeroReport(Faction.Enemy));
            InitRankPage(_battleReportRoot);
        }

        private void InitCamera()
        {
            var cameraGameobject = GameObject.FindGameObjectWithTag("CanvasCamera");
            if (cameraGameobject.TryGetComponent(out Camera camera))
            {
                _canvas.worldCamera = camera;
            }
            
        }

        private void InitBattlePlayerInfo(bool playerWon, int level)
        {
            _battlePlayerInfo.Init(level, playerWon);
        }

        private void InitFactionStats(BattleReportStatsItem playerStats, BattleReportStatsItem enemyStats)
        {
            _playerStats.InitFactionStats(playerStats.DamageDealt, playerStats.DamageTaken, playerStats.DamageBlocked, playerStats.Healed);
            _enemyStats.InitFactionStats(enemyStats.DamageDealt, enemyStats.DamageTaken, enemyStats.DamageBlocked, enemyStats.Healed);
        }

        private void InitHeroStats(BattleReportUnit playerHero, BattleReportUnit enemyHero)
        {
            if (playerHero != null)
            {
                _playerStats.InitHeroStats(true, playerHero.DamageDealt, playerHero.DamageTaken, playerHero.DamageBlocked, playerHero.HealthHealed);
                _playerStats.InitHeroPictureAndName(playerHero.Icon, playerHero.ID.Name);
            }
            else
            {
                _playerStats.InitHeroStats(false);
            }

            if (enemyHero != null)
            {
                _enemyStats.InitHeroStats(true, enemyHero.DamageDealt, enemyHero.DamageTaken, enemyHero.DamageBlocked, enemyHero.HealthHealed);
                _enemyStats.InitHeroPictureAndName(enemyHero.Icon, enemyHero.ID.Name);
            }
            else
            {
                _enemyStats.InitHeroStats(false);
            }
        }

        private void InitRankPage(BattleReportRoot reportRoot)
        {
            _battleReportRankPage.Init(reportRoot); 
        }
    }
}
