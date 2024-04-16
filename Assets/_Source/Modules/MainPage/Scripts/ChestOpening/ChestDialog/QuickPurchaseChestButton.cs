using PersistentData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MainPage
{
    internal class QuickPurchaseChestButton : MonoBehaviour
    {
        internal Action OnChestPurchased;

        [SerializeField] private Button _button;
        [SerializeField] private FloatVariableSO _currencyForPurchase;
        [SerializeField] private Text _priceText;
        [SerializeField] private GameObject _instantPurchaseDialog;

        private bool _hasEnoughMoney;
        private float _price;
        internal void Init(float amount)
        {
            _priceText.text = amount.ToString();
            _price = amount;
            UpdateAvailability();
        }

        private void UpdateAvailability(float amount = 0)
        {
            _hasEnoughMoney = _price <= _currencyForPurchase.Value;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
            _currencyForPurchase.OnValueChanged += UpdateAvailability;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleClick);
            _currencyForPurchase.OnValueChanged -= UpdateAvailability;
        }

        private void HandleClick()
        {
            if (_hasEnoughMoney)
            {
                OnChestPurchased?.Invoke();
            }
            else
            {
                Instantiate(_instantPurchaseDialog);
            }
        }
    }
}
