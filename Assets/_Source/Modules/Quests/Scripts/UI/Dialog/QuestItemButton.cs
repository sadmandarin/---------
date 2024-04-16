using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class QuestItemButton : MonoBehaviour
    {
        internal Action OnClaimedReward, OnStartQuest;

        [SerializeField] private Button _button;
        [SerializeField] private LeanPhrase _claimPhrase, _startPhrase, _claimedPhrase;
        [SerializeField] private Sprite _startBg, _claimBg;
        [SerializeField] private Text _text;
        
        private QuestItemButtonState _state;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            if (_state == QuestItemButtonState.Claimed)
                return;

            if (_state == QuestItemButtonState.Claim)
            {
                OnClaimedReward?.Invoke();
                return;
            }

            if (_state == QuestItemButtonState.Start) 
            {
                OnStartQuest?.Invoke();
                return;
            }
        }

        internal void SetState(bool isReadyToBeCollected, bool alreadyClaimed)
        {
            QuestItemButtonState state = QuestItemButtonState.Start;
            if (alreadyClaimed)
                state = QuestItemButtonState.Claimed;
            if (isReadyToBeCollected && alreadyClaimed == false)
                state = QuestItemButtonState.Claim;

            _state = state;

            ChangeVisual();
        }

        private void ChangeVisual()
        {
            if (_state == QuestItemButtonState.Claimed)
            {
                _button.image.enabled = false;
                _text.text = LeanLocalization.GetTranslationText(_claimedPhrase.name);
                return;
            }

            if (_state == QuestItemButtonState.Start)
            {
                _button.image.sprite = _startBg;
                _text.text = LeanLocalization.GetTranslationText(_startPhrase.name);
            }

            if (_state == QuestItemButtonState.Claim)
            {
                _button.image.sprite = _claimBg;
                _text.text = LeanLocalization.GetTranslationText(_claimPhrase.name);
            }

        }
    }

    internal enum QuestItemButtonState
    {
        Start,
        Claim,
        Claimed
    }

}
