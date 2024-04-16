using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class UserLevelBar : MonoBehaviour
    {
        [SerializeField] private Text _progressText;
        [SerializeField] private Image _progressBar;
        [SerializeField] private Text _levelText;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Sprite _rewardReaySprite, _rewardNotReadySprite;
        [SerializeField] private IntVariableSO _experience;
        [SerializeField] private ExperienceConfigSO _experienceConfig;
        [SerializeField] private ExperienceRewardCollection _rewardCollection;
        [SerializeField] private Button _dialogButton;
        [SerializeField] private UserLevelRewardListDialog _rewardsDialog;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private MainPageObjectsHider _objectsHider;
        [SerializeField] private GameObject _effectWhenRewardReady;
        [SerializeField] private GameObject _bannerWhenNewRewardReady;

        private void OnEnable()
        {
            UpdateUserBar();
            _dialogButton.onClick.AddListener(ShowDialog);
            _experience.OnValueChanged += UpdateUserBar;
        }

        private void OnDisable()
        {
            _dialogButton.onClick.RemoveListener(ShowDialog);
            _experience.OnValueChanged -= UpdateUserBar;
        }

        private void ShowDialog()
        {
            var dialog = Instantiate(_rewardsDialog);
            dialog.InitCamera(_canvas.worldCamera);
            _objectsHider.ToggleVisibility();
            dialog.DialogClosed += _objectsHider.ToggleVisibility;
            dialog.RewardClaimed += () => UpdateUserBar();
        }

        private void UpdateUserBar(int experience = 0)
        {
            int currentLevelIndex = _experienceConfig.GetUserLevelByExperience(_experience.Value);
            int nextLevelIndex = Mathf.Clamp(currentLevelIndex + 1, 0, _experienceConfig.ExperienceLevelList.Count - 1);
            ExperienceLevel nextLevelData = _experienceConfig.ExperienceLevelList[nextLevelIndex];
            ExperienceLevel CurrentLevelData = _experienceConfig.ExperienceLevelList[currentLevelIndex];
            var experienceForNextLevel = nextLevelData.ExperienceToNextLevel;
            var currentExperience = _experience.Value;
            _levelText.text = (currentLevelIndex + 1).ToString();
            _progressText.text = currentExperience + "/" + experienceForNextLevel;
            var progressScale = _progressBar.transform.localScale;
            progressScale.x = Mathf.Clamp((float)(currentExperience- CurrentLevelData.ExperienceToNextLevel)/(float)(experienceForNextLevel - CurrentLevelData.ExperienceToNextLevel), 0, 1);
            _progressBar.transform.localScale = progressScale;
            _rewardImage.sprite = nextLevelData.Reward.Icon;
            ToggleBarVisual(AreThereUncollectedRewards(currentLevelIndex));
        }

        private bool AreThereUncollectedRewards(int currentLevel)
        {
            if (_rewardCollection.IsInitialized == false)
                _rewardCollection.InitWithStartingData();

            for (int i = 0; i < currentLevel + 1; i++)
            {
                if (_rewardCollection.IsCollected(i) == false)
                    return true;
            }
            return false;
        }

        private void ToggleBarVisual(bool rewardIsReady)
        {
            _buttonImage.sprite = rewardIsReady ? _rewardReaySprite : _rewardNotReadySprite;
            _effectWhenRewardReady.SetActive(rewardIsReady);
            _bannerWhenNewRewardReady.SetActive(rewardIsReady);
        }
    }
}
