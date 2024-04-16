using System;
using UnityEngine;
using UnityEngine.UI;
using YandexSDK;

namespace ShopPage
{
    internal class InstantPurchaseItem : MonoBehaviour
    {
        internal Action OnItemPurchased;

        [SerializeField] private Image _iconImage;
        [SerializeField] private Text _priceText;
        [SerializeField] private Text _valueText;
        [SerializeField] private Button _button;

        private ShopItemFloatVariableData _data;

        internal void SetUp(ShopItemFloatVariableData itemData)
        {
            _data = itemData;
            _iconImage.sprite = itemData.Icon;
            _valueText.text = itemData.Quantity.ToString();
            _priceText.text = itemData.Price.ToString();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandlePurchaseButtonPressed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandlePurchaseButtonPressed);
        }

        private void HandlePurchaseButtonPressed()
        {
            YandexManager.Instance.PurchaseConsumable(HandlePurchase, _data.YandexId);
        }

        private void HandlePurchase()
        {
            _data.VariableToIncrease.Value += _data.Quantity;
            YandexMetrika.Event(_data.YandexId);
            OnItemPurchased?.Invoke();
        }
    }
}
