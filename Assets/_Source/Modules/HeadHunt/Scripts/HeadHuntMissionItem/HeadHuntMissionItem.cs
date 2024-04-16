using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeadHunt
{
    internal class HeadHuntMissionItem : MonoBehaviour
    {
        internal Action<LevelVariable> OnMissionButtonClicked;

        [SerializeField] private Image _icon;
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private HeadHuntMissionItemRewards _rewards;
        [SerializeField] private HeadHuntAttackInformation _attackInformation;
        [SerializeField] private HeadHuntMissionDailyActivity _dailyActivity;
        [SerializeField] private GameObject _finishedView;
        [SerializeField] private GameObject _hardMission;
        [SerializeField] private Button _button;

        private LevelVariable _levelVariable;
        private ExtraRewardsBaseConfig _rewardsConfig;
        private IntVariableSO _timesRemaining;
        private int _lastLevel;

        internal void Init(Sprite icon, string title, string description, LevelVariable levelVariable, ExtraRewardsBaseConfig rewardsConfig,
                           IntVariableSO timesRemaining, bool isHard)
        {
            _rewardsConfig = rewardsConfig;
            _timesRemaining = timesRemaining;

            _levelVariable = levelVariable;
            _lastLevel = _levelVariable.Value;
            _levelVariable.OnValueChanged += HandleMissionCompleted;

            _icon.sprite = icon;
            _titleText.text = title;
            _descriptionText.text = description;
            UpdateView();
            _hardMission.SetActive(isHard);
        }

        private void UpdateView()
        {
            _rewards.Init(_levelVariable, _rewardsConfig);
            _attackInformation.Init(_levelVariable, _timesRemaining);
            _dailyActivity.Init(_levelVariable, _timesRemaining);
            _finishedView.SetActive(_timesRemaining.Value == 0);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
            
        }

        private void HandleMissionCompleted(int newLevel)
        {
            if (_lastLevel == 10 && newLevel == 1)
            {
                _timesRemaining.Value -= 1;
            }
            _lastLevel = newLevel;
            UpdateView();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            _levelVariable.OnValueChanged -= HandleMissionCompleted;
        }

        private void OnButtonClicked()
        {
            OnMissionButtonClicked?.Invoke(_levelVariable);
        }
    }
}
