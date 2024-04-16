using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class UnlockingRequirementStat : MonoBehaviour
    {
        [SerializeField] private Text _unlockedAtLevel;

        private const string UnlockingRequirement = "UnlockingRequirement";
        private const string NormalGettingRequirement = "NormalGettingRequirement";

        internal void Set(bool isLocked, int level)
        {
            if (isLocked)
                _unlockedAtLevel.text = Lean.Localization.LeanLocalization.GetTranslationText(UnlockingRequirement) + ": " + level;
            else
            {
                _unlockedAtLevel.text = Lean.Localization.LeanLocalization.GetTranslationText(NormalGettingRequirement);
            }
        }
    }
}