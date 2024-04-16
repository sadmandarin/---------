using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class DoubleSpeedIAPButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private DoubleSpeedButton _doubleSpeedButton;
        [SerializeField] private GameObject _parent;
        [SerializeField] private YandexProduct _doubleSpeedProduct;

        private void OnEnable()
        {
            _button.onClick.AddListener(BuyIAP);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(BuyIAP);
        }

        private void BuyIAP()
        {
            YandexManager.Instance.PurchaseConsumable(ConsumationLogic, _doubleSpeedProduct.YandexId);
        }

        private void ConsumationLogic()
        {
            _doubleSpeedProduct.GetRewardForProduct();
            _parent.SetActive(false);
            _doubleSpeedButton.Init();
        }
    }
}
