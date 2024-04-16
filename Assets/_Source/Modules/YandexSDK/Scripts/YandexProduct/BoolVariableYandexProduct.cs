using PersistentData;
using UnityEngine;
using YandexSDK;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Yandex/DoubleSpeedButton")]
    public class BoolVariableYandexProduct : YandexProduct
    {
        [SerializeField] private BoolVariableSO _boolVariable;

        public override void GetRewardForProduct()
        {
            _boolVariable.Value = true;

            YandexMetrika.Event(YandexId);
        }
    }
}
