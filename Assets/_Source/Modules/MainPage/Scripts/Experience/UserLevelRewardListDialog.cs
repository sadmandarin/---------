using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class UserLevelRewardListDialog : MonoBehaviour
    {
        internal Action DialogClosed, RewardClaimed;

        [SerializeField] private UserLevelRewardItem _itemPrefab;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private IntVariableSO _experience;
        [SerializeField] private ExperienceConfigSO _config;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _closeButton;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private ExperienceRewardCollection _experienceRewardCollection;

        private UserLevelRewardItem _rewardItem = null;

        private void OnEnable()
        {
            if (_experienceRewardCollection.IsInitialized == false)
                _experienceRewardCollection.InitWithStartingData();
            SetUpLevelRewards();
            _closeButton.onClick.AddListener(CloseDialog);
        }

        private void CloseDialog()
        {
            DialogClosed?.Invoke();
            Destroy(gameObject);
        }

        internal void InitCamera(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }

        private void SetUpLevelRewards()
        {
            var rewards = _config.ExperienceLevelList;
            int currentExperience = _experience.Value;
            int userLevel = _config.GetUserLevelByExperience(_experience.Value);

            

            for (int i = 0; i < rewards.Count; i++)
            {
                ExperienceLevel level = rewards[i];
                var item = Instantiate(_itemPrefab, _contentParent);
                bool isLocked = userLevel < i;

                var nextExperienceRequirement = rewards[Mathf.Clamp(i + 1, 0, rewards.Count - 1)].ExperienceToNextLevel;
                var currentExperienceRequirement = level.ExperienceToNextLevel;
                var previousExperienceRequirement = rewards[Mathf.Clamp(i - 1, 0, rewards.Count - 1)].ExperienceToNextLevel;

                float nextExperienceDelta = (nextExperienceRequirement - currentExperienceRequirement) / 2f;
                float previousExperienceDelta = (currentExperienceRequirement - previousExperienceRequirement) / 2f;

                float progress = 0f;
                if (currentExperience > currentExperienceRequirement)
                {
                    progress = (float)(currentExperience - currentExperienceRequirement) /
                        (currentExperienceRequirement + nextExperienceDelta - currentExperienceRequirement);
                    progress = Mathf.Clamp(progress * 0.5f + 0.5f, 0, 1);
                }
                else
                {
                    float numerator = (float)(currentExperience - currentExperienceRequirement + previousExperienceDelta);
                    float denominator = (float)(currentExperienceRequirement - currentExperienceRequirement + previousExperienceDelta);
                    progress = numerator == 0 ? 0 : numerator / denominator;
                    progress = Mathf.Clamp(progress * 0.5f, 0, 1);
                    if (i == 0)
                        progress = Mathf.Clamp(progress + 0.5f, 0, 1);
                }

                var isCollected = _experienceRewardCollection.IsCollected(i);
                item.SetUp(level.Reward, level.QuantityOfReward, i + 1, level.ExperienceToNextLevel, isLocked, isCollected, progress);

                if (i == 0)
                    item.MakeExtraDownProgress();
                if (i == rewards.Count - 1)
                    item.MakeExtraUpProgress();

                if (i == userLevel && progress < 1 || currentExperience == currentExperienceRequirement + nextExperienceDelta)
                    item.SetExperienceBubbleData(currentExperience);
                else if (i == userLevel + 1 && progress > 0)
                    item.SetExperienceBubbleData(currentExperience);
                else
                    item.TurnOffBubble();

                if (i == userLevel)
                    _rewardItem = item;

                item.RewardClaimed += HandleRewardClaimed;
            }

            StartCoroutine(FocusCoroutine());
        }

        private void FocusOnCurrentLevel()
        {
            if (_rewardItem != null)
                ScrollViewFocusFunctions.FocusOnItem(_scrollRect, _rewardItem.RectTransform);
        }

        private IEnumerator FocusCoroutine()
        {
            yield return new WaitForEndOfFrame();
            FocusOnCurrentLevel();
        }

        private void HandleRewardClaimed()
        {
            RewardClaimed?.Invoke();
        }
    }
}
