using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    internal class DailyTroopsDialog : MonoBehaviour
    {
        [SerializeField] private Text _quantityCountText;
        [SerializeField] private Text _quantityItemText;
        [SerializeField] private Text _quantityGemsText;
        [SerializeField] private Text _priceText;
        [SerializeField] private Button _claimButton;
        [SerializeField] private Button _moreButton;
        [SerializeField] private Button _lessButton;
        [SerializeField] private Button _maxButton;
        [SerializeField] private Image _troopImage;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UnitAdder _unitAdder;

        private int _quantity, _pricePerUnit;
        private string _troopName;

        private void OnEnable()
        {
            _moreButton.onClick.AddListener(AddMore);
            _lessButton.onClick.AddListener(MakeLess);
            _maxButton.onClick.AddListener(MaxAmount);
            _claimButton.onClick.AddListener(MakePurchase);
        }

        private void MakePurchase()
        {
            var totalCost = _quantity * _pricePerUnit;
            for (int i = 0; i < _quantity; i++)
            {
                _unitAdder.AddUnit(_troopName, 1);
            }
            _gems.Value -= totalCost;
            _quantity = 0;
            UpdateTexts();
            UpdateButtonsAvailability();
        }

        private void MaxAmount()
        {
            _quantity = Mathf.FloorToInt(_gems.Value / _pricePerUnit);
            UpdateTexts();
            UpdateButtonsAvailability();
        }

        internal void Init(Sprite troopIcon, int pricePerUnit, string troopName, Camera canvasCamera)
        {
            _troopImage.sprite = troopIcon;
            _pricePerUnit = pricePerUnit;
            _troopName = troopName;
            _canvas.worldCamera = canvasCamera;
            UpdateButtonsAvailability();
        }

        private void AddMore()
        {
            _quantity += 1;
            UpdateTexts();
            UpdateButtonsAvailability();
        }

        private void MakeLess()
        {
            _quantity -= 1;
            UpdateTexts();
            UpdateButtonsAvailability();
        }

        private void UpdateTexts()
        {
            _quantityItemText.text = _quantity.ToString();
            _quantityCountText.text = _quantity.ToString();
            _priceText.text = (_quantity * _pricePerUnit).ToString();
        }

        private void UpdateButtonsAvailability()
        {
            int totalPrice = _quantity * _pricePerUnit;
            if (totalPrice + _pricePerUnit > _gems.Value) 
            {
                _moreButton.interactable = false;
            }
            else
            {
                _moreButton.interactable = true;
            }
            if (_quantity == 0)
            {
                _lessButton.interactable = false;
            }
            else
            {
                _lessButton.interactable = true;
            }
        }
    }
}
