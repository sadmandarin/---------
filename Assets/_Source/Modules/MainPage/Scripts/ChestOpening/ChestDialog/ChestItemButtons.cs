using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestItemButtons : MonoBehaviour
    {
        internal Action OnChestOpened, OnChestPurchased, OnAddButtonPressed;

        [SerializeField] private Button _openButton;
        [SerializeField] private QuickPurchaseChestButton _purchaseButton;
        [SerializeField] private Button _rewardButton;

        internal void SetPrice(int amount)
        {
            _purchaseButton.Init(amount);
        }



        private void OnEnable()
        {
            _openButton.onClick.AddListener(RaiseOnOpenChest);
            _purchaseButton.OnChestPurchased += (RaiseOnPurchaseChest);
            _rewardButton.onClick.AddListener(RaiseOnAddButtonPressed);
        }

        private void OnDisable()
        {
            _openButton.onClick?.RemoveListener(RaiseOnOpenChest);
            _purchaseButton.OnChestPurchased -= (RaiseOnPurchaseChest);
            _rewardButton.onClick.RemoveListener(RaiseOnAddButtonPressed);
        }

        private void RaiseOnAddButtonPressed()
        {
            OnAddButtonPressed?.Invoke();
        }

        private void RaiseOnPurchaseChest()
        {
            OnChestPurchased?.Invoke();
        }

        private void RaiseOnOpenChest()
        {
            OnChestOpened?.Invoke();
        }
    }
}
