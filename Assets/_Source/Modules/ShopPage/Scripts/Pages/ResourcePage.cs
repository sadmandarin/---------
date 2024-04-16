using MainPage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopPage
{
    internal class ResourcePage : MonoBehaviour
    {
        [SerializeField] private ShopResourcesItem _shopItemPrefab;
        [SerializeField] private List<ShopItemFloatVariableData> _gemsItems;
        [SerializeField] private List<ShopItemFloatVariableData> _coinsItems;
        [SerializeField] private Transform _gemsContentParent;
        [SerializeField] private Transform _coinsContentParent;
        [SerializeField] private ItemCollectionAnimation _collectingAnimation;

        private void Awake()
        {
            foreach (var shopItemData in _gemsItems)
            {
                var shopItem = Instantiate(_shopItemPrefab, _gemsContentParent);
                shopItem.SetUp(shopItemData);
                shopItem.OnPurchasedCoins += CollectCoinsAnimation;
                shopItem.OnPurchasedGems += CollectGemsAnimation;
            }

            foreach (var shopItemData in _coinsItems)
            {
                var shopItem = Instantiate(_shopItemPrefab, _coinsContentParent);
                shopItem.SetUp(shopItemData);
                shopItem.OnPurchasedCoins += CollectCoinsAnimation;
                shopItem.OnPurchasedGems += CollectGemsAnimation;
            }
        }

        private void CollectGemsAnimation()
        {
            _collectingAnimation.PlayAnimations(true, false);
        }

        private void CollectCoinsAnimation()
        {
            _collectingAnimation.PlayAnimations(false, true);
        }
    }
}
