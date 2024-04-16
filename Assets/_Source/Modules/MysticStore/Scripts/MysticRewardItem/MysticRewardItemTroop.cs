using PersistentData;
using UnitsData;
using UnityEngine;

namespace MysticStore
{
    [CreateAssetMenu(menuName = "MysticStore/TroopItem")]

    internal class MysticRewardItemTroop : MysticRewardItemBase
    {
        [SerializeField] private UnitViewSO _unitView;
        [SerializeField] private int _troopStars = 1;
        [SerializeField] private int _price;
        [SerializeField] private UnitAdder _unitAdder;

        public override void ClaimReward() => _unitAdder.AddUnit(_unitView.Name.ToString(), _troopStars);

        public override string Description()
        {
            return Lean.Localization.LeanLocalization.GetTranslationText(_unitView.Name.ToString());
        }

        public override Sprite Icon() => _unitView.Icon;
        public override bool IsHero() => false;
        public override int Price() => _price;
        public override int Rarity() => _unitView.Rarity;
        public override int TroopStars() => _troopStars;
    }
}
