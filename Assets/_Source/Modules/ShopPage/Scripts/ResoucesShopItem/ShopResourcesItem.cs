using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YandexSDK;

namespace ShopPage
{
    internal class ShopResourcesItem : MonoBehaviour
    {
        internal Action OnPurchasedCoins, OnPurchasedGems;

        [SerializeField] private Image _itemIcon;
        [SerializeField] private Text _itemQuantity;
        [SerializeField] private Text _itemPrice;
        [SerializeField] private Text _itemYanPrice;
        [SerializeField] private GameObject _normalPriceParent;
        [SerializeField] private GameObject _inAppPurchaseParent;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private GameObject _instantGemsPurchase;

        private ShopItemFloatVariableData _data;
        private bool _hasEnoughMoney;

        internal void SetUp(ShopItemFloatVariableData itemData)
        {
            _data = itemData;
            _itemIcon.sprite = itemData.Icon;
            _itemQuantity.text = itemData.Quantity.ToString();
            _itemPrice.text = itemData.Price.ToString();
            _itemYanPrice.text = itemData.Price.ToString();
            _normalPriceParent.SetActive(!_data.IsForYan);
            _inAppPurchaseParent.SetActive(_data.IsForYan);
            
            UpdateAvailability();
        }

        private void UpdateAvailability(float gems = 0)
        {
            if (_data == null)
                return;

            if (_data.IsForYan)
            {
                _purchaseButton.interactable = true;
            }
            else
            {
                _hasEnoughMoney = _data.Price <= _gems.Value;
            }
        }

        private void OnEnable()
        {
            _purchaseButton.onClick.AddListener(OnBuyClick);
            _gems.OnValueChanged += UpdateAvailability;

            UpdateAvailability();
        }

        private void OnDisable()
        {
            _purchaseButton.onClick.RemoveListener(OnBuyClick);
            _gems.OnValueChanged -= UpdateAvailability;
        }

        private void OnBuyClick()
        {
            if (_data.IsForYan)
            {
                YandexManager.Instance.PurchaseConsumable(HandlePurchase, _data.YandexId);
            }
            else if (_hasEnoughMoney)
            {
                _gems.Value -= _data.Price;
                HandlePurchase();
            }
            else
            {
                Instantiate(_instantGemsPurchase);
            }
        }

        private void HandlePurchase()
        {
            _data.VariableToIncrease.Value += _data.Quantity;
            YandexMetrika.Event(_data.YandexId);
            bool buyingGems = _data.VariableToIncrease == _gems;
            if (buyingGems)
                OnPurchasedGems?.Invoke();
            else
                OnPurchasedCoins?.Invoke();
        }
    }
}
