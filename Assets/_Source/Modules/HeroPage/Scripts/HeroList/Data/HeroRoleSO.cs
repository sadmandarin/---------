using Lean.Localization;
using UnityEngine;

namespace HeroPage
{
    [CreateAssetMenu(menuName = "HeroPageView/Role")]
    internal class HeroRoleSO : ScriptableObject
    {
        internal string Description => Lean.Localization.LeanLocalization.GetTranslationText(_description.name);
        [field: SerializeField] internal Sprite Icon { get; private set; }

        [SerializeField] private LeanPhrase _description;
    }
}
