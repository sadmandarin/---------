using PersistentData;
using UnityEngine;

namespace MysticStore
{
    [CreateAssetMenu(menuName = "MysticStore/ChestItem")]
    internal class MysticRewardItemChest : MysticRewardItemBase
    {
        [SerializeField] private ChestVariableSO _chest;
        [SerializeField] private int _price;
        [SerializeField] private int _rarity;

        public override void ClaimReward() => _chest.AddChest();

        public override string Description()
        {
            return _chest.Name;
        }

        public override Sprite Icon() => _chest.ChestIcon;

        public override bool IsHero() => false;

        public override int Price() => _price;

        public override int Rarity() => _rarity;

        public override int TroopStars() => 0;
    }
}
