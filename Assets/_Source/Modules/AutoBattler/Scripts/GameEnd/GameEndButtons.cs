using PersistentData;
using Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class GameEndButtons : MonoBehaviour
    {
        internal Action OnEndScreenDismissed;

        [SerializeField] private Button _rewardedButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private RewardAssigner _rewardAssigner;
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private QuestItemDescriptionSO _quest;

        private int _moneyWon;
        private bool _died;
        private LevelConfigBaseSO _levelConfig;

        internal void Init(bool died, int moneyWon, LevelConfigBaseSO levelConfig)
        {
            _levelConfig = levelConfig;
            _died = died;
            _moneyWon = moneyWon;

            _retryButton.gameObject.SetActive(died);
            _continueButton.gameObject.SetActive(!died);
            _rewardedButton.gameObject.SetActive(moneyWon > 0);

            _retryButton.onClick.AddListener(OnRetryButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _rewardedButton.onClick.AddListener(OnRewardedButtonClicked);
        }

        private void OnRetryButtonClicked()
        {
            ClaimRewards();
            OnEndScreenDismissed.Invoke();
        }

        internal void OnContinueButtonClicked()
        {
            ClaimRewards();
            OnEndScreenDismissed.Invoke();
        }

        private void OnRewardedButtonClicked()
        {
            YandexManager.Instance.WatchRewardedVideoWithClicker(ClaimRewarded);
        }

        private void ClaimRewarded()
        {
            _rewardAssigner.AssignRewards(_moneyWon * 3);
            _questsCompleter.CompleteQuest(_quest);
            OnEndScreenDismissed.Invoke();
        }

        private void ClaimRewards()
        {
            _rewardAssigner.AssignRewards(_moneyWon);
        }
    }
}
