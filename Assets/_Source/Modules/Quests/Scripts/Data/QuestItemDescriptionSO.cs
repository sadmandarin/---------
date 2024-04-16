using Lean.Localization;
using PersistentData;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(menuName = "Quests/QuestDescrpition")]
    public class QuestItemDescriptionSO : ScriptableObject
    {
        internal QuestRewardSO Reward => _reward;
        internal int CompletionRequirement => _timesToCompleteQuestRequirement;
        internal string Description => LeanLocalization.GetTranslationText(_questDescriptionPhrase.name);

        public int ProgressQuantity { get => _progressQuantity; private set => _progressQuantity = value; }
        public VoidEventChannelSO MovingTrigger { get => _movingTrigger; private set => _movingTrigger = value; }

        [SerializeField] private LeanPhrase _questDescriptionPhrase;
        [SerializeField] private QuestRewardSO _reward;
        [SerializeField] private int _progressQuantity;
        [SerializeField] private int _timesToCompleteQuestRequirement;
        [SerializeField] private VoidEventChannelSO _movingTrigger;

        internal string Title()
        {
            string result = LeanLocalization.GetTranslationText(_questDescriptionPhrase.name);
            return string.Format(result, _timesToCompleteQuestRequirement);
        }

    }
}
