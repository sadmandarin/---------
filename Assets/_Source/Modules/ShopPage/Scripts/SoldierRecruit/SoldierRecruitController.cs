using Lean.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;
using YandexSDK;

namespace ShopPage
{
    internal class SoldierRecruitController : MonoBehaviour
    {
        [SerializeField] private SoldierRecruitUiItem _uiItemPrefab;
        [SerializeField] private List<SoldierRecruitItemData> _itemsData;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private SoldierRecruitConfirmDialog _confirmDialog;
        [SerializeField] private SoldierRecruitRewardDialog _rewardDialog;
        [SerializeField] private Canvas _canvas;

        private void Start()
        {
            for (int i = 0; i < _itemsData.Count; i++)
            {
                var item = Instantiate(_uiItemPrefab, _contentParent);
                item.SetUp(_itemsData[i]);
                item.OnPurchasePressed += HandleOnPurchasePressed;
            }    
        }

        private void HandleOnPurchasePressed(SoldierRecruitItemData itemData)
        {
            var dialog = Instantiate(_confirmDialog);
            dialog.InitDialog(_canvas.worldCamera);
            dialog.SetUp(itemData);
            dialog.OnPurchased += HandleOnPurchased;
        }

        private void HandleOnPurchased(SoldierRecruitItemData data)
        {
            YandexManager.Instance.PurchaseConsumable(() => ClaimRewards(data), data.YandexId);
        }

        private void ClaimRewards(SoldierRecruitItemData data)
        {
            var rewardDialog = Instantiate(_rewardDialog);
            rewardDialog.InitCamera(_canvas.worldCamera);
            rewardDialog.SpawnCards(data.NumberOfUnitsThatWillBeUnlocked, data.LevelOfTroops);
            YandexMetrika.Event(data.YandexId);
        }
    }
}
