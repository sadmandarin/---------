using System;
using System.Collections.Generic;
using UnityEngine;
using PersistentData;
using static UnityEngine.UI.CanvasScaler;

namespace AutoBattler
{
    public class AutoBattlerRoot : MonoBehaviour
    {
        public Action OnBattleStarted;

        public Action OnPlayerWon, OnPlayerLost;
        public bool HasBattleStarted { get; private set; }
        internal LevelConfigBaseSO LevelConfig => _levelConfig;

        [SerializeField] private AutoBattlerUnitsManager _unitManager;
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private BattleReportRoot _battleReport;
        [SerializeField] private StrengthBalanceInfo _strengthBalanceInfo;
        [SerializeField] private LevelStartButton _levelStartButton;
        [SerializeField] private InBattleCoinsCounter _coinsCounter;
        [SerializeField] private HeroInitializer _heroInitializer;
        [SerializeField] private ActiveSkillButton _activeSkillButton;
        [SerializeField] private CanvasSwitcher _canvasSwitcher;
        [SerializeField] private AudioSource _winningCheersSound, _losingDespairSound;
        [SerializeField] private TroopSpawner _troopSpawner;
        [SerializeField] private InBattleLevelTxt _inBattleLevelText;
        [SerializeField] private HeroHPInfo _heroHpInfo;
        [SerializeField] private BattleCameraChanger _cameraChanger;
        
        private LevelConfigBaseSO _levelConfig;

        public void ResetBattle()
        {
            _canvasSwitcher.ResetCanvases();
            _troopSpawner.ResetTroopSpawner();
            _unitManager.ClearUnits();
            _levelLoader.ClearTerrain();
            _unitManager.NoMoreTargetsLeft -= HandleNoMoreTargetsLeft;
            _unitManager.EnemyUnitDied -= HandleEnemyUnitDied;
            _heroHpInfo.Reset();
            _cameraChanger.ResetCamera();
            HasBattleStarted = false;
        }

        public void AddPlayerUnits(List<GameObject> playerUnits, int level)
        {
            _unitManager.AddPlayerUnitsWithoutRepeating(playerUnits, level);
        }

        public void UpdatePlayerUnitsStats()
        {
            _unitManager.UpdateUnitsStats();
            _strengthBalanceInfo.CalculateAndSetStats(_unitManager.Units);
        }
        public void InitHero(Transform heroPosition)
        {
            _heroInitializer.InitializeHero(heroPosition);
            if (_heroInitializer.Hero != null)
            {
                heroPosition.gameObject.SetActive(true);
                _unitManager.AddPlayerHero(_heroInitializer.Hero);
                _heroHpInfo.Init(_heroInitializer.Hero.GetComponent<Health>(), _heroInitializer.Hero.transform);
                _heroInitializer.Hero.UnitDied -= HeroDied;
                _heroInitializer.Hero.UnitDied += HeroDied;
            }
            else
            {
                heroPosition.gameObject.SetActive(false);
            }
        }
        public void Init(LevelConfigBaseSO levelConfig)
        {
            _levelConfig = levelConfig;
            _levelLoader.Init(levelConfig);
            var enemies = _levelLoader.SpawnEnemies();

            _unitManager.AddEnemyUnits(enemies);
            _unitManager.NoMoreTargetsLeft -= HandleNoMoreTargetsLeft;
            _unitManager.NoMoreTargetsLeft += HandleNoMoreTargetsLeft;
            _unitManager.EnemyUnitDied -= HandleEnemyUnitDied;
            _unitManager.EnemyUnitDied += HandleEnemyUnitDied;

            InitLevelStartButton(levelConfig.CurrentLevel, levelConfig.IsMission);
            _inBattleLevelText.InitText(levelConfig.CurrentLevel, levelConfig.IsMission);
            _unitManager.UpdateUnitsStats();
        }

        internal void ReInit()
        {
            _levelLoader.Init(_levelConfig);
            var enemies = _levelLoader.SpawnEnemies();
            _unitManager.ClearEnemyUnits();
            _unitManager.AddEnemyUnits(enemies);
            _unitManager.UpdateUnitsStats();
            _strengthBalanceInfo.CalculateAndSetStats(_unitManager.Units);
        }

        private void HandleEnemyUnitDied(AutoBattlerUnit unitThatDied)
        {
            _coinsCounter.IncreaseCounter(unitThatDied.RewardForKilling);
        }

        private void HandleNoMoreTargetsLeft(bool playerWon)
        {
            if (playerWon)
            {
                _winningCheersSound.Play();
                Invoke(nameof(InvokePlayerWon), 1f);
            }
            else
            {
                _losingDespairSound.Play();
                Invoke(nameof(InvokePlayerLost), 1f);
            }
            Time.timeScale = 1;
            _troopSpawner.Deactivate();
        }

        private void InitLevelStartButton(int level, bool isMission)
        {
            _levelStartButton.Init(level, isMission, StartBattle);
        }

        public void StartBattle()
        {
            HasBattleStarted = true;
            if (_heroInitializer.Hero != null)
                _heroInitializer.Hero.InitHeroSkills(_activeSkillButton);
            else
                _activeSkillButton.Hide();
            _unitManager.InitUnitsForBattle();
            _unitManager.MovePlayerUnitsToAnotherParent();
            _battleReport.Init(_unitManager.Units, _levelConfig.CurrentLevel);
            _canvasSwitcher.SwitchToBattleCanvas();

            _troopSpawner.Init(_levelConfig.CanSpawnTroops);
            _troopSpawner.Activate();

            _cameraChanger.ChangeCamera();

            OnBattleStarted?.Invoke();
        }

        private void InvokePlayerWon()
        {
            OnPlayerWon?.Invoke();
        }

        private void InvokePlayerLost()
        {
            OnPlayerLost?.Invoke();
        }

        private void HeroDied(AutoBattlerUnit unit)
        {
            _heroHpInfo.ToggleView(false);
            _heroHpInfo.Reset();
            _activeSkillButton.Hide();
        }
    }
}
