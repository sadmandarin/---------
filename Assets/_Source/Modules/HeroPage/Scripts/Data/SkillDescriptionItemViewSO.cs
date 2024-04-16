using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroPage
{
    [CreateAssetMenu(menuName = "HeroPageView/SkillDescriptionItem")]
    internal class SkillDescriptionItemViewSO : ScriptableObject
    {
        internal string Title => LeanLocalization.GetTranslationText(_title.name);

        [field: SerializeField] internal Sprite Icon { get; private set; }
        
        [SerializeField] private LeanPhrase _title;
    }
}
