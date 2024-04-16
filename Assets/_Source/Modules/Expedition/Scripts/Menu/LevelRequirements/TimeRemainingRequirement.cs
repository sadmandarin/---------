using Lean.Localization;
using UnityEngine;

namespace Expedition
{
    [CreateAssetMenu(menuName = "Expedition/LevelRequirement")]
    internal class TimeRemainingRequirement : ConquestLevelRequirementBase
    {
        [SerializeField] private LeanPhrase _requirementPhrase;

        internal override string GetLevelRequirementText(float[] valuesRequirements)
        {
            var localizedString = LeanLocalization.GetTranslationText(_requirementPhrase.name);
            return string.Format(localizedString, valuesRequirements[0]);
        }

        internal override bool IsRequirementFullfilled(float[] valuesRequirements, LevelController levelController)
        {
            return valuesRequirements[0] >= levelController.timerSecond;
        }
    }
}
