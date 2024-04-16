using PersistentData;
using UnitsData;
using UnityEngine;

namespace MysticStore
{
    [CreateAssetMenu(menuName = "MysticStore/HeroItem")]
    internal class MysticRewardItemHero : MysticRewardItemBase
    {
        [SerializeField] private HeroCollection _heroCollection;
        [SerializeField] private HeroPresentationSO _heroPresentation;
        [SerializeField] private int _price;
        public override void ClaimReward() => _heroCollection.AddHero(_heroPresentation.HeroName.ToString());

        public override string Description()
        {
            return Lean.Localization.LeanLocalization.GetTranslationText(_heroPresentation.HeroName.ToString());
        }

        public override Sprite Icon() => _heroPresentation.BoxIcon;

        public override bool IsHero() => true;

        public override int Price() => _price;

        public override int Rarity() => _heroPresentation.Rarity;

        public override int TroopStars() => 0;
    }
}
