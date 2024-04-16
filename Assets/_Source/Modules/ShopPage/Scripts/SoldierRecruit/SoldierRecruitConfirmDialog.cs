using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    internal class SoldierRecruitConfirmDialog : MonoBehaviour
    {
        internal Action<SoldierRecruitItemData> OnPurchased;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _totalCardsText;
        [SerializeField] private LeanPhrase _totalCardsPhrase;
        [SerializeField] private Text _priceText;
        [SerializeField] private Text _cardsQuantityText;
        [SerializeField] private Image _starsImage;
        [SerializeField] private Sprite _singleStarSprite;
        [SerializeField] private Sprite _fourStarSprite;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _confirmButton;

        private SoldierRecruitItemData _itemData;

        internal void InitDialog(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }

        internal void SetUp(SoldierRecruitItemData itemData)
        {
            _itemData = itemData;
            var totalCards = itemData.NumberOfUnitsThatWillBeUnlocked;
            var stars = itemData.LevelOfTroops;
            var price = itemData.Price;
            string chestName = LeanLocalization.GetTranslationText(itemData.ChestTitlePhrase.name);

            _titleText.text = chestName;
            string localizedCardsPhrase = LeanLocalization.GetTranslationText(_totalCardsPhrase.name);
            _totalCardsText.text = string.Format(localizedCardsPhrase, totalCards);
            _priceText.text = price.ToString();
            _starsImage.sprite = stars == 1 ? _singleStarSprite : _fourStarSprite;
            _cardsQuantityText.text = "X" + totalCards;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseDialog);
            _confirmButton.onClick.AddListener(HandleOnConfirmPressed);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseDialog);
            _confirmButton.onClick.RemoveListener(HandleOnConfirmPressed);
        }

        private void HandleOnConfirmPressed()
        {
            Destroy(gameObject);
            OnPurchased?.Invoke(_itemData);
        }

        private void CloseDialog()
        {
            Destroy(gameObject);
        }
    }
}
