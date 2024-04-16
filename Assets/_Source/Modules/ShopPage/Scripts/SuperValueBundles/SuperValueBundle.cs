using MainPage;
using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YandexSDK;

namespace ShopPage
{
    internal class SuperValueBundle : MonoBehaviour
    {
        [SerializeField] private SuperValueBundleData _bundleData;
        [SerializeField] private Button _claimButton;
        [SerializeField] private SoldierRecruitRewardDialog _rewardDialog;
        [SerializeField] private FloatVariableSO _gemsVariable, _coinsVariable;
        [SerializeField] private ItemCollectionAnimation _collectionAnimation;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Text _priceText;

        private void OnEnable()
        {
            _claimButton.onClick.AddListener(HandleOnClick);
            SetPrice();
        }

        private void OnDisable()
        {
            _claimButton.onClick.RemoveListener(HandleOnClick);
        }

        private void SetPrice()
        {
            _priceText.text = _bundleData.Price.ToString();
        }

        private void HandleOnClick()
        {
            YandexManager.Instance.PurchaseConsumable(ClaimRewards, _bundleData.YandexId);
        }

        private void ClaimRewards()
        {
            var dialog = Instantiate(_rewardDialog);
            dialog.InitCamera(_canvas.worldCamera);
            dialog.SpawnCards(_bundleData.TroopsToAdd, 1);
            _gemsVariable.Value += _bundleData.Gems;
            _coinsVariable.Value += _bundleData.Coins;
            _collectionAnimation.PlayAnimations(true, true);
            YandexMetrika.Event(_bundleData.YandexId);
        }
    }
}
