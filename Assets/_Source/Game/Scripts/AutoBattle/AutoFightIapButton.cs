using PersistentData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class AutoFightIapButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BoolVariableYandexProduct _purchased;
        [SerializeField] private GameObject _parent;
        [SerializeField] private IntVariableSO _autoFightVariable;

        private void OnEnable()
        {
            _button.onClick.AddListener(BuyProduct);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(BuyProduct);
        }

        private void BuyProduct()
        {
            YandexManager.Instance.PurchaseConsumable(HandleOnPurchased, _purchased.YandexId);
        }

        private void HandleOnPurchased()
        {
            _purchased.GetRewardForProduct();
            _autoFightVariable.Value = 100;
            _parent.SetActive(false);
        }
    }
}
