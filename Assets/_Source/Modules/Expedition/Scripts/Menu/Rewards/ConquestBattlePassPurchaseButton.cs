using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestBattlePassPurchaseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private int _price;
        [SerializeField] private BoolVariableSO _purchasedBattlePass;
        [SerializeField] private GameObject _lock, _unlock;
        [SerializeField] private ConquestRewardsList _rewardsList;
        [SerializeField] private GameObject _instantPurchaseDialog;

        private void OnEnable()
        {
            UpdateButtonAvailability();
            _button.onClick.AddListener(PurchaseBattlePass);
            _gems.OnValueChanged += UpdateButtonAvailability;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PurchaseBattlePass);
            _gems.OnValueChanged -= UpdateButtonAvailability;
        }

        private void PurchaseBattlePass()
        {
            if (_gems.Value < _price)
            {
                Instantiate(_instantPurchaseDialog);
            }
            else
            {
                _gems.Value -= _price;
                _purchasedBattlePass.Value = true;
                UpdateButtonAvailability();
                _rewardsList.UpdateItems();
            }
            
        }

        private void UpdateButtonAvailability(float gems = 0)
        {
            bool battlePassPurchased = _purchasedBattlePass.Value;
            bool enoughGems = _gems.Value >= _price;
            _lock.SetActive(battlePassPurchased == false);
            _unlock.SetActive(battlePassPurchased);
            //_button.interactable = (enoughGems);
        }
    }
}
