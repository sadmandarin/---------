using Lean.Localization;
using PersistentData;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    internal class DailyItemBase : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _gemsImage;
        [SerializeField] private Text _itemNameText;
        [SerializeField] private Text _itemRarityText;
        [SerializeField] private Text _priceText;
        [SerializeField] private Image _bgImage;
        [SerializeField] private Sprite[] _borderRarities;
        [SerializeField] private LeanPhrase _freePhrase;
        [SerializeField] private LeanPhrase[] _itemRarityPhrases;
        [SerializeField] private Color[] _itemRarityColors;
        [SerializeField] private Button _claimButton;
        [SerializeField] private DailyTroopsDialog _dialogPrefab;
        [SerializeField] private NotEnoughResourcesDialog _notEnoughResourcesPrefab;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private ShopPageRoot _shopPageRoot;

        private int _price;
        private Sprite _troopIcon;
        private string _troopName;

        private void OnEnable()
        {
            _claimButton.onClick.AddListener(OpenExchangeDialog);
        }

        private void OnDisable()
        {
            _claimButton.onClick.RemoveListener(OpenExchangeDialog);
        }

        private void OpenExchangeDialog()
        {
            if (_gems.Value < _price)
            {
                var dialog = Instantiate(_notEnoughResourcesPrefab);
                dialog.InitCamera(_canvas.worldCamera);
                dialog.OnBuyMorePressed += HandleOnBuyMorePressed;
            }
            else
            {
                var dialog = Instantiate(_dialogPrefab);
                dialog.Init(_troopIcon, _price, _troopName, _canvas.worldCamera);
            }
        }

        private void HandleOnBuyMorePressed()
        {
            _shopPageRoot.OpenResoucesGemsPanel();
        }

        internal void SetUp(Sprite icon, string translatedName, int rarity, int price, string defaultName)
        {
            _price = price;
            _troopIcon = icon;
            _troopName = defaultName;
            if (price == 0)
            {
                _priceText.text = LeanLocalization.GetTranslationText(_freePhrase.name);
                _gemsImage.gameObject.SetActive(false);
            }
            else
            {
                _priceText.text = price.ToString();
                _gemsImage.gameObject.SetActive(true);
            }
            _iconImage.sprite = icon;
            _itemNameText.text = translatedName;
            _bgImage.sprite = _borderRarities[rarity];
            string itemRarity = LeanLocalization.GetTranslationText(_itemRarityPhrases[rarity].name);
            _itemRarityText.text = itemRarity;
            _itemRarityText.color = _itemRarityColors[rarity];
        }
    }
}
