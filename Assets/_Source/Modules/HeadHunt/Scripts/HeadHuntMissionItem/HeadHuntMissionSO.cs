using Lean.Localization;
using PersistentData;
using UnityEngine;

namespace HeadHunt
{
    [CreateAssetMenu(menuName = "HeadHunt/MissionData")]
    internal class HeadHuntMissionSO : ScriptableObject
    {
        internal string Title => Lean.Localization.LeanLocalization.GetTranslationText(TitlePhrase.name);
        internal string Description => Lean.Localization.LeanLocalization.GetTranslationText(DescriptionPhrase.name);

        [field: SerializeField] internal Sprite Icon { get; private set; }
        [field: SerializeField] internal LeanPhrase TitlePhrase{ get; private set; }
        [field: SerializeField] internal LeanPhrase DescriptionPhrase{ get; private set; }
        [field: SerializeField] internal ExtraRewardsBaseConfig ExtraRewardsConfig { get; private set; }
        [field: SerializeField] internal LevelVariable Level { get; private set; }
        [field: SerializeField] internal IntVariableSO TimesRemainingVariable{ get; private set; }
        [field: SerializeField] internal bool IsHard { get; private set; }
    }
}
