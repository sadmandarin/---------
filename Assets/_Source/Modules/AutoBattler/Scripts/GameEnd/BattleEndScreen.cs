using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class BattleEndScreen : MonoBehaviour
    {
        internal Action OnEndScreenDismissed;

        [SerializeField] private Transform _contentParent;
        [SerializeField] private BattleRemainArmy _battleRemainArmy;
        [SerializeField] private GameEndRewardArea _gameEndRewardArea;
        [SerializeField] private GameEndButtons _gameEndButtons;
        [SerializeField] private GameObject _winViewParent;
        [SerializeField] private GameObject _loseViewParent;
        [SerializeField] private Text _winText;
        [SerializeField] private Text _loseText;

        private BattleRemainArmy _battleRemainArmyInGame;
        private GameEndButtons _gameEndButton;
        
        internal void Init(bool hasWon, bool showBattleRemains,  int numberOfTroops, int rewardForKillingTroops,
                          LevelConfigBaseSO levelConfig, BattleReportRoot reportRoot = null)
        {
            _winViewParent.SetActive(hasWon);
            _loseViewParent.SetActive(!hasWon);
            _winText.gameObject.SetActive(hasWon);
            _loseText.gameObject.SetActive(!hasWon);


            if (showBattleRemains)
            {
                _battleRemainArmyInGame = Instantiate(_battleRemainArmy, _contentParent);
                _battleRemainArmyInGame.Init(numberOfTroops, reportRoot, hasWon);
            }
            
            var rewardsArea = Instantiate(_gameEndRewardArea, _contentParent);
            rewardsArea.Init(rewardForKillingTroops, levelConfig, hasWon, out int totalMoneyWon);

            _gameEndButton = Instantiate(_gameEndButtons, _contentParent);
            _gameEndButton.Init(!hasWon, totalMoneyWon, levelConfig);
            
            _gameEndButton.OnEndScreenDismissed += HandleOnEndScreenDismissed;

            if (hasWon)
                levelConfig.IncreaseLevel();
        }

        private void HandleOnEndScreenDismissed()
        {
            OnEndScreenDismissed?.Invoke();
        }

        internal void CollectRewards()
        {
            _gameEndButton.OnContinueButtonClicked();
        }

        internal void HideGameEndButtons()
        {
            _gameEndButton.gameObject.SetActive(false);
        }

        internal void ShowGameEndButtons()
        {
            _gameEndButton.gameObject.SetActive(true);
        }
    }
}
