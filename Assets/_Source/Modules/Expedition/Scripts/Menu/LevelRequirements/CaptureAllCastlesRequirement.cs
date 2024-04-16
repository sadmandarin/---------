using Lean.Localization;
using UnityEngine;

namespace Expedition
{
    [CreateAssetMenu(menuName = "Expedition/CatpureAllCastlesRequirement")]
    internal class CaptureAllCastlesRequirement : ConquestLevelRequirementBase
    {
        [SerializeField] private LeanPhrase _requirementPhrase;

        internal override string GetLevelRequirementText(float[] valuesRequirements)
        {
            var localizedString = LeanLocalization.GetTranslationText(_requirementPhrase.name);
            return localizedString;
        }

        internal override bool IsRequirementFullfilled(float[] valuesRequirements, LevelController levelController)
        {
            return levelController.AllCastleCatpured();
        }
    }
}
