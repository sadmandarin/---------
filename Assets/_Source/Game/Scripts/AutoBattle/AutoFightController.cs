using AutoBattler;
using PersistentData;
using System;
using System.Collections;
using UnityEngine;

namespace Legion
{
    internal class AutoFightController : MonoBehaviour
    {
        internal bool Enabled { get => _enabled; private set => _enabled = value; }

        [SerializeField] private GameEndCanvas _gameEndCanvas;
        [SerializeField] private StartBattleButton _startBattleButton;
        [SerializeField] private AutoBattlerRoot _autoBattlerRoot;
        [SerializeField] private IntVariableSO _autoBattlerTimes;
        [SerializeField] private BoolVariableSO _purchased;
        [SerializeField] private VoidEventChannelSO _stopAutoFight;
        [SerializeField] private MenuSwitcher _menuSwitcher;

        private bool _enabled;

        private void OnEnable()
        {
            _stopAutoFight.OnEventRaised += Deactivate;
        }

        private void OnDisable()
        {
            _stopAutoFight.OnEventRaised -= Deactivate;
        }

        internal void Activate()
        {
            _enabled = true;
            _gameEndCanvas.OnLevelEnded += HandleLevelEnded;
            if (_autoBattlerRoot.HasBattleStarted == false)
                _autoBattlerRoot.StartBattle();
        }

        internal void Deactivate()
        {
            _enabled = false;
            _gameEndCanvas.OnLevelEnded -= HandleLevelEnded;
        }

        private void HandleLevelEnded(bool playerWon, LevelConfigBaseSO levelConfig)
        {
            if (_purchased.Value == false)
                _autoBattlerTimes.Value -= 1;
            
            if (_autoBattlerTimes.Value == 0)
            {
                Deactivate();
                return;
            }
                
            if (playerWon)
            {
                StartCoroutine(MoveToNextLevelCoroutine(levelConfig));
            }
            else
            {
                Deactivate();
            }
            
        }

        private IEnumerator MoveToNextLevelCoroutine(LevelConfigBaseSO levelConfig)
        {
            _gameEndCanvas.HideNextButtons();
            yield return new WaitForSeconds(1f);
            if (_enabled == false)
            {
                _gameEndCanvas.ShowNextButtons();
                yield break;
            }
                
            _gameEndCanvas.CollectRewards();
            //_gameEndCanvas.HandleOnEndScreenDismissed();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            if (levelConfig.IsNextLevelAvailable() == false)
                yield break;
            _menuSwitcher.SwitchToBattle(levelConfig);
            //_startBattleButton.StartBattle();
            yield return new WaitForEndOfFrame();
            _autoBattlerRoot.StartBattle();
        }
    }
}
