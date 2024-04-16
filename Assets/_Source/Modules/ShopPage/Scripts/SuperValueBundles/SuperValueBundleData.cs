using UnityEngine;
using YandexSDK;

namespace ShopPage
{
    [CreateAssetMenu(menuName = "Shop/SuperValueBundle")]
    public class SuperValueBundleData : YandexProduct
    {
        [field:SerializeField] public int Gems { get; private set; }
        [field:SerializeField] public int Coins { get; private set; }
        [field:SerializeField] public int TroopsToAdd { get; private set; }
        [field:SerializeField] public int Price { get; private set; }
        
        [SerializeField] private SuperValueBundleConfig _config;

        public override void GetRewardForProduct()
        {
            _config.AddSoldierAndResouces(TroopsToAdd, Coins, Gems);
            YandexMetrika.Event(YandexId);
        }
    }
}
