using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using YandexSDK;

namespace ShopPage
{
    [CreateAssetMenu(menuName = "Shop/Resources")]
    public class ShopItemFloatVariableData : YandexProduct
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Quantity { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public FloatVariableSO VariableToIncrease { get; private set; }
        [field: SerializeField] public bool IsForYan { get; private set; }

        public override void GetRewardForProduct()
        {
            VariableToIncrease.Value += Quantity;
            YandexMetrika.Event(YandexId);
        }
    }
}
