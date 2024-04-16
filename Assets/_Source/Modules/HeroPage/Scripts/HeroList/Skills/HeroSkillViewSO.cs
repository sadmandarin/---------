using Lean.Localization;
using System;
using UnityEngine;

namespace HeroPage
{
    [CreateAssetMenu(menuName = "HeroPageView/HeroSkill")]
    internal class HeroSkillViewSO : ScriptableObject
    {
        internal string Title => LeanLocalization.GetTranslationText(_title.name);
        internal string Description => LeanLocalization.GetTranslationText(_description.name);

        [field:SerializeField] internal Sprite Icon { get; private set; }
        [field:SerializeField] internal bool IsPassive { get; private set; }
        [field:SerializeField] internal SkillDamageType DamageType { get; private set; }
        [field:SerializeField] internal HeroSkillDescription[] SkillDescriptions { get; private set; }

        [SerializeField] private LeanPhrase _title, _description;
        
        
    }

    [Serializable]
    internal struct HeroSkillDescription
    {
        public LeanPhrase LeanPhraseName;
        public float[] Values;
        public string ValueSuffix;
    }
}
