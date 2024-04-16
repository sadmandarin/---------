using PersistentData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class LuckyCardCollectButton : MonoBehaviour
    {
        internal Action<int> OnButtonPressed;

        [SerializeField] private Button _button;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private Text _priceText;
        [SerializeField] private GameObject _instantGemsPurchaseDialog;

        private int _price;
        private bool _hasEnoughMoney;

        internal void InitPrice(int price)
        {
            _price = price;
            _priceText.text = _price.ToString();
            UpdateAvailability();
            _gems.OnValueChanged += UpdateAvailability;
        }

        private void UpdateAvailability(float obj = 0)
        {
            _hasEnoughMoney = (_price <= _gems.Value);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(RaiseEvent);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RaiseEvent);
        }

        private void RaiseEvent()
        {
            if (_hasEnoughMoney)
                OnButtonPressed?.Invoke(_price);
            else
                Instantiate(_instantGemsPurchaseDialog);
        }
    }

}
