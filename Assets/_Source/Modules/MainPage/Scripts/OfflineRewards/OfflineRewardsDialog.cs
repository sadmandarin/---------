using Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class OfflineRewardsDialog : MonoBehaviour
    {
        internal Action DialogClosed, ClaimButtonPressed;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Text _coinsText;
        [SerializeField] private Text _gemsText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _claimButton;
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private QuestItemDescriptionSO _quest;

        internal void Init(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }

        internal void SetValues(int coins, int gems)
        {
            _coinsText.text = coins.ToString();
            _gemsText.text = gems.ToString();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseDialog);
            _claimButton.onClick.AddListener(ClaimRewards);
        }

        private void ClaimRewards()
        {
            ClaimButtonPressed?.Invoke();
            _questsCompleter.CompleteQuest(_quest);
            Destroy(gameObject);
        }

        internal void CloseDialog()
        {
            DialogClosed?.Invoke();
            Destroy(gameObject);
        }
    }
}
