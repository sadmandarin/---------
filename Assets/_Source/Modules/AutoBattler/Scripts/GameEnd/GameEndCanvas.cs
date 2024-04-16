using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AutoBattler
{
    public class GameEndCanvas : MonoBehaviour
    {
        public Action<bool, LevelConfigBaseSO> OnLevelEnded;
        public Action OnEndScreenDismissed;

        [SerializeField] private bool _showBattleRemains;
        [SerializeField] private Transform _parentToInstantiateEndScreen;
        [SerializeField] private GameObject _blackScreen;
        [SerializeField] private AutoBattlerRoot _autoBattlerRoot;
        [SerializeField] private AutoBattlerUnitsManager _unitsManager;
        [SerializeField] private BattleReportRoot _reportRoot;
        [SerializeField] private BattleEndScreen _battleEndScreen;
        [SerializeField] private InBattleCoinsCounter _coinsCounter;
        [SerializeField] private AudioSource _winSound, _loseSound;
        [SerializeField] private CanvasSwitcher _canvasSwitcher;

        private GameObject _gameEndScreen;

        private void OnEnable()
        {
            _autoBattlerRoot.OnPlayerWon += OnPlayerWon;
            _autoBattlerRoot.OnPlayerLost += OnPlayerLost;
        }

        private void OnDisable()
        {
            _autoBattlerRoot.OnPlayerWon -= OnPlayerWon;
            _autoBattlerRoot.OnPlayerLost -= OnPlayerLost;
        }

        private void OnPlayerWon()
        {
            _blackScreen.gameObject.SetActive(true);
            var gameEndScreen = Instantiate(_battleEndScreen, _parentToInstantiateEndScreen);
            gameEndScreen.Init(true, _showBattleRemains, _unitsManager.UnitsRemainingAlive, _coinsCounter.CoinsReward,
                _autoBattlerRoot.LevelConfig, _reportRoot);
            gameEndScreen.OnEndScreenDismissed += HandleOnEndScreenDismissed;
            _gameEndScreen = gameEndScreen.gameObject;
            _winSound.Play();
            _canvasSwitcher.SwitchToEndCanvas();
            OnLevelEnded?.Invoke(true, _autoBattlerRoot.LevelConfig);
            
        }

        private void OnPlayerLost()
        {
            _blackScreen.gameObject.SetActive(true);
            var gameEndScreen = Instantiate(_battleEndScreen, _parentToInstantiateEndScreen);
            gameEndScreen.Init(false, _showBattleRemains, _unitsManager.UnitsRemainingAlive, _coinsCounter.CoinsReward,
                _autoBattlerRoot.LevelConfig, _reportRoot);
            gameEndScreen.OnEndScreenDismissed += HandleOnEndScreenDismissed;
            _gameEndScreen = gameEndScreen.gameObject;
            _loseSound.Play();
            _canvasSwitcher.SwitchToEndCanvas();
            OnLevelEnded?.Invoke(false, _autoBattlerRoot.LevelConfig);
        }

        public void HandleOnEndScreenDismissed()
        {
            OnEndScreenDismissed.Invoke();
            _blackScreen.gameObject.SetActive(false);
            _coinsCounter.ResetCoins();
            _reportRoot.ResetData();
            Destroy(_gameEndScreen);
            _canvasSwitcher.ResetCanvases();
        }

        public void CollectRewards()
        {
            _gameEndScreen.GetComponent<BattleEndScreen>().CollectRewards();
        }

        public void HideNextButtons()
        {
            _gameEndScreen.GetComponent<BattleEndScreen>().HideGameEndButtons();
        }

        public void ShowNextButtons()
        {
            _gameEndScreen.GetComponent<BattleEndScreen>().ShowGameEndButtons();
        }

    }
}
