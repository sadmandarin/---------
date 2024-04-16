using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroPage
{
    [CreateAssetMenu(menuName = "HeroPageView/Skill")]
    internal class TroopSkillView : ScriptableObject
    {
        internal string Title => LeanLocalization.GetTranslationText(_title.name);
        internal string Description => LeanLocalization.GetTranslationText(_description.name);

        [field: SerializeField] internal SkillDescriptionData[] DescriptionItems;
        [field: SerializeField] internal Sprite Icon { get; private set; }

        [SerializeField] private LeanPhrase _title;
        [SerializeField] private LeanPhrase _description;
        
    }
}
