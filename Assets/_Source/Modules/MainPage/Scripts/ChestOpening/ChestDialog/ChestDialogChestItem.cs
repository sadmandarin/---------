using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestDialogChestItem : MonoBehaviour
    {
        [SerializeField] private Image _chestImage;
        [SerializeField] private Text _chestQuantityText;
        [SerializeField] private Text _chestTitleText;
        
        [SerializeField] private ChestItemTimer _chestTimer;
        [SerializeField] private ChestItemButtons _chestItemButtons;
        [SerializeField] private ChestItemView _view;
        [SerializeField] private ChestItemPurchases _chestPurchases;

        private ChestVariableSO _chest;

        private void OnEnable()
        {
            _chestTimer.ChestUnlocked += HandleChestUnlocked;
            _chestItemButtons.OnChestOpened += HandleChestOpened;
            _chestItemButtons.OnChestPurchased += HandleChestPurchased;
            _chestItemButtons.OnAddButtonPressed += HandleAdButtonPressed;
        }

        private void OnDisable()
        {
            _chestTimer.ChestUnlocked -= HandleChestUnlocked;
            _chestItemButtons.OnChestOpened -= HandleChestOpened;
            _chestItemButtons.OnChestPurchased -= HandleChestPurchased;
            _chestItemButtons.OnAddButtonPressed -= HandleAdButtonPressed;
        }

        internal void SetUp(ChestVariableSO chest)
        {
            _chest = chest;

            bool playerHasChest = chest.QuantityOfChests > 0;
            _view.SetChestView(playerHasChest == false ? ChestState.None : ChestState.NotReady);
            _chestTitleText.text = chest.Name;
            if (playerHasChest)
            {
                _chestQuantityText.text = chest.QuantityOfChests.ToString();

                var fullTimeToOpen = chest.GetFullTimeToOpen();
                _chestTimer.Init(fullTimeToOpen, chest.SavedTime);
                var timeLeft = _chestTimer.TimeLeft;

                var priceForQuickPurchase = chest.GetCostToOpenImmediately(timeLeft);
                _chestItemButtons.SetPrice(priceForQuickPurchase);
                
                _chestPurchases.Init(chest, priceForQuickPurchase);
            }           
        }

        private void HandleChestUnlocked()
        {
            _view.SetChestView(ChestState.Ready);
        }

        private void HandleChestOpened()
        {
            _chestPurchases.OpenChest();
        }

        private void HandleChestPurchased()
        {
            _chestPurchases.PurchaseAndOpenChest();
        }

        private void HandleAdButtonPressed()
        {
            YandexManager.Instance.WatchRewardedVideoWithClicker(() =>
            {
                _chestTimer.RemoveOneHourFromTimer();
                var newCostToOpen = _chest.GetCostToOpenImmediately(_chestTimer.TimeLeft);
                _chestPurchases.UpdatePrice(newCostToOpen);
                _chestItemButtons.SetPrice(newCostToOpen);
            });
        }
    }
}
