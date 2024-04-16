using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnitsData;
using UnityEngine;

namespace HeroPage
{
    [CreateAssetMenu(menuName = "HeroPageView/Hero")]
    internal class HeroViewSO : ScriptableObject
    {
        internal string Title => LeanLocalization.GetTranslationText(HeroPresentation.HeroName.ToString() + "Title");
        [field: SerializeField] internal HeroPresentationSO HeroPresentation { get; private set; }
        [field: SerializeField] internal bool IsUnlocked { get; private set; }
        [field: SerializeField] internal bool IsMagicAttacker { get; private set; }
        [field: SerializeField] internal GameObject Prefab => HeroPresentation.PrefabForPresentionInUi;
        [field: SerializeField] internal UnitStatsData Stats { get; private set; }
        [field: SerializeField] internal HeroSkillViewSO[] Skills { get; private set; }
        [field: SerializeField] internal HeroRaceSO Race { get; private set; }
        [field: SerializeField] internal HeroRoleSO Role { get; private set; }

    }
}
