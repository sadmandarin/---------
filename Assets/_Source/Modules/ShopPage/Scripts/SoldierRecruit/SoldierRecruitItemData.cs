using Lean.Localization;
using UnityEngine;
using PersistentData;
using YandexSDK;

namespace ShopPage
{
    [CreateAssetMenu(menuName = "Shop/SoldierRecruitItem")]
    public class SoldierRecruitItemData : YandexProduct
    {
        [field: SerializeField] public LeanPhrase ChestTitlePhrase { get; private set; }
        [field: SerializeField] public int LevelOfTroops { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite ChestIcon { get; private set; }
        [field: SerializeField] public int NumberOfUnitsThatWillBeUnlocked { get; private set; }

        [field: SerializeField] private SoldierRecruitConfig _config;

        public override void GetRewardForProduct()
        {
            _config.AddNewTroops(NumberOfUnitsThatWillBeUnlocked, LevelOfTroops);
            YandexMetrika.Event(YandexId);
        }
    }
}
