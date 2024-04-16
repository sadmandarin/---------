using Lean.Localization;
using UnityEngine;

namespace Expedition
{
    [CreateAssetMenu(menuName = "Expedition/CaptureCertainCastles")]
    internal class CaptureCertainCastlesRequirement : ConquestLevelRequirementBase
    {
        [SerializeField] private LeanPhrase _requirementPhrase;

        internal override string GetLevelRequirementText(float[] valuesRequirements)
        {
            var localizedString = LeanLocalization.GetTranslationText(_requirementPhrase.name);
            Debug.Log("Localized string: " + localizedString);
            return string.Format(localizedString, valuesRequirements[0], valuesRequirements[1]);
        }

        internal override bool IsRequirementFullfilled(float[] valuesRequirements, LevelController levelController)
        {
            return valuesRequirements[0] <= levelController.countCaptureCastleOfLevel;
        }
    }
}
