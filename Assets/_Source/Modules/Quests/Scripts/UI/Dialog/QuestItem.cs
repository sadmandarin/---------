using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Quests
{
    internal class QuestItem : MonoBehaviour
    {
        internal Action<QuestItemDescriptionSO> OnClaimedReward, OnStartedQuest;
        internal bool Collected => _collected;
        internal bool IsReadyToBeCollected => _isReadyToBeCollected;

        public QuestItemDescriptionSO QuestDescription { get => _questDescription; private set => _questDescription = value; }

        [SerializeField] private Text _descriptionText;
        [SerializeField] private Text _requirementText;
        [SerializeField] private Transform _parentForRewardItems;
        [SerializeField] private GameObject _rewardsParent;
        [SerializeField] private RewardItem _rewardItemPrefab;
        [SerializeField,FormerlySerializedAs("_progressText")] private Text _progressQuantityText;

        [SerializeField] private List<VisualChanger> _visualChangers;
        [SerializeField] private VisualChanger _bgImageChanger;
        [SerializeField] private QuestItemButton _questButton;
        
        private bool _collected;
        private bool _isReadyToBeCollected;
        private QuestItemDescriptionSO _questDescription;

        private void OnEnable()
        {
            _questButton.OnStartQuest += StartQuest;
            _questButton.OnClaimedReward += ClaimReward;
        }

        private void OnDisable()
        {
            _questButton.OnStartQuest -= StartQuest;
            _questButton.OnClaimedReward -= ClaimReward;
        }

        private void ClaimReward()
        {
            //ChangeViewToClaimed();
            OnClaimedReward?.Invoke(_questDescription);
        }

        internal void ChangeViewToClaimed()
        {
            _collected = true;
            ToggleVisual(false);
            _bgImageChanger.ToggleVisual(false);
        }

        private void StartQuest()
        {
            OnStartedQuest?.Invoke(_questDescription);
        }

        internal void SetUp(QuestItemDescriptionSO questDescription, int timesCompleted, bool collected)
        {
            _questDescription = questDescription;
            _collected = collected;

            int clampedTimesCompleted = Mathf.Clamp(timesCompleted, 0, questDescription.CompletionRequirement);
            string requirementText = $"({clampedTimesCompleted}/{questDescription.CompletionRequirement})";
            _requirementText.text = requirementText;

            string descriptionText = string.Format(questDescription.Description, questDescription.CompletionRequirement);
            _descriptionText.text = descriptionText;

            _rewardsParent.SetActive(!collected);
            foreach (var reward in questDescription.Reward.ExtraRewards)
            {
                var rewardItem = Instantiate(_rewardItemPrefab, _parentForRewardItems);
                rewardItem.SetUp(reward.Quantity, reward.ExtraReward.Icon);
            }

            _progressQuantityText.text = questDescription.ProgressQuantity.ToString();

            ToggleVisual(collected == false);

            _isReadyToBeCollected = timesCompleted >= questDescription.CompletionRequirement && collected == false;

            _questButton.SetState(_isReadyToBeCollected, _collected);
            _bgImageChanger.ToggleVisual(_isReadyToBeCollected);
        }

        private void ToggleVisual(bool isActive)
        {
            foreach (var visual in _visualChangers)
            {
                visual.ToggleVisual(isActive);
            }
            _questButton.SetState(_isReadyToBeCollected, _collected);
        }
    }

}
