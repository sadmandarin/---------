using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class UserLevelRewardItem : MonoBehaviour
    {
        internal Action RewardClaimed;

        [SerializeField] private Image _rewardImage;
        [SerializeField] private Text _rewardQuantityText;
        [SerializeField] private Text _rewardLevelText;
        [SerializeField] private Text _rewardExperienceText;
        [SerializeField] private Image _progressBarImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Sprite _lockedSprite;
        [SerializeField] private Sprite _collectedSprite;
        [SerializeField] private Button _claimButton;
        [SerializeField] private GameObject _experienceBubble;
        [SerializeField] private Text _currentExperienceText;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private ExperienceRewardCollection _rewardCollection;
        [SerializeField] private GameObject _extraDownProgress, _extraUpProgress;

        private ExtraRewardBase _reward;
        private int _rewardQuantity, _rewardLevel;

        public RectTransform RectTransform { get => _rectTransform; private set => _rectTransform = value; }

        internal void SetUp(ExtraRewardBase reward, int quantity, int level, int experience, bool isLocked, bool isCollected, float progress)
        {
            _reward = reward;
            _rewardLevel = level - 1;
            _rewardQuantity = quantity;
            _rewardImage.sprite = reward.Icon;
            _rewardQuantityText.text = quantity.ToString();
            _rewardLevelText.text = level.ToString();
            _rewardExperienceText.text = experience.ToString();
            if (isCollected)
            {
                SwitchVisualToCollected();
            }
            else if (isLocked)
            {
                _lockImage.gameObject.SetActive(true);
                _lockImage.sprite = _lockedSprite;
                _claimButton.gameObject.SetActive(false);
            }
            else
            {
                _lockImage.gameObject.SetActive(false);
                _claimButton.gameObject.SetActive(true);
                _claimButton.onClick.AddListener(ClaimReward);
            }
            var progressScale = _progressBarImage.transform.localScale;
            progressScale.y = progress;
            _progressBarImage.transform.localScale = progressScale;
        }

        internal void TurnOffBubble()
        {
            _experienceBubble.gameObject.SetActive(false);
        }

        internal void SetExperienceBubbleData(float currentExperience)
        {
            Canvas.ForceUpdateCanvases();
            _currentExperienceText.text = currentExperience.ToString();
            var bubblePosition = _experienceBubble.transform.localPosition;
            bubblePosition.y = -_rectTransform.sizeDelta.y / 2 + _rectTransform.sizeDelta.y * _progressBarImage.transform.localScale.y;
            _experienceBubble.transform.localPosition = bubblePosition;
        }

        internal void MakeExtraDownProgress()
        {
            _extraDownProgress.SetActive(true);
        }

        internal void MakeExtraUpProgress()
        {
            _extraUpProgress.SetActive(true);
        }

        private void ClaimReward()
        {
            _reward.ClaimReward(_rewardQuantity);
            _rewardCollection.CollectReward(_rewardLevel);
            SwitchVisualToCollected();
            RewardClaimed?.Invoke();
        }

        private void SwitchVisualToCollected()
        {
            _lockImage.gameObject.SetActive(true);
            _lockImage.sprite = _collectedSprite;
            _claimButton.gameObject.SetActive(false);
        }
    }
}
