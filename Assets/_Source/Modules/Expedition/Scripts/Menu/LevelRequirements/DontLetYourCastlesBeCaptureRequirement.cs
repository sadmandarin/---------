using Lean.Localization;
using UnityEngine;

namespace Expedition
{
    [CreateAssetMenu(menuName = "Expedition/DontLetYourCastlesBeCaptureRequirement")]
    internal class DontLetYourCastlesBeCaptureRequirement : ConquestLevelRequirementBase
    {
        [SerializeField] private LeanPhrase _requirementPhrase;

        internal override string GetLevelRequirementText(float[] valuesRequirements)
        {
            var localizedString = LeanLocalization.GetTranslationText(_requirementPhrase.name);
            return string.Format(localizedString, valuesRequirements[0]);
        }

        internal override bool IsRequirementFullfilled(float[] requirementValues, LevelController levelController)
        {
            return requirementValues[0] >= levelController.capturePlayerCastle;
        }
    }
}
