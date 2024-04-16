using Lean.Localization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    internal class SoldierRecruitUiItem : MonoBehaviour
    {
        internal Action<SoldierRecruitItemData> OnPurchasePressed;

        [SerializeField] private Text _titleText;
        [SerializeField] private Text _levelOfTroopsText;
        [SerializeField] private Text _priceText;
        [SerializeField] private Image _chestImage;
        [SerializeField] private LeanPhrase _troopsInChestPhrase;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private SoldierRecruitConfirmDialog _confirmDialog;

        private SoldierRecruitItemData _itemData;

        internal void SetUp(SoldierRecruitItemData itemData)
        {
            _itemData = itemData;

            _titleText.text = LeanLocalization.GetTranslationText(itemData.ChestTitlePhrase.name);
            string localizedText = LeanLocalization.GetTranslationText(_troopsInChestPhrase.name);
            _levelOfTroopsText.text = string.Format(localizedText, itemData.LevelOfTroops);
            _priceText.text = itemData.Price.ToString();
            _chestImage.sprite = itemData.ChestIcon;
        }

        private void OnEnable()
        {
            _purchaseButton.onClick.AddListener(HandleOnPurchase);
        }

        private void OnDisable()
        {
            _purchaseButton.onClick.RemoveListener(HandleOnPurchase);
        }

        private void HandleOnPurchase()
        {
            OnPurchasePressed?.Invoke(_itemData); 
        }
    }
}
