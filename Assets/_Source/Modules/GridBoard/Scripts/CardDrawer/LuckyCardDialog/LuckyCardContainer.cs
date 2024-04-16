using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class LuckyCardContainer : MonoBehaviour
    {
        internal Action<LuckyCardContainer, int> OnLuckyCardPurchased;
        internal string Name => _name;
        internal int Level => _level;

        [SerializeField] private Text _nameText;
        [SerializeField] private Image _troopIcon;
        [SerializeField] private Image _bgFrame;
        [SerializeField] private Image _bgName;
        [SerializeField] private Sprite[] _bgFrameRarities;
        [SerializeField] private Sprite[] _bgNameRarities;
        [SerializeField] private Color[] _nameColorRarities;
        [SerializeField] private Image _starPrefab;
        [SerializeField] private Transform _starParent;
        [SerializeField] private CardAppearingAnimation _animation;
        [SerializeField] private LuckyCardCollectButton _collectButton;
        [SerializeField] private LuckyConfigSO _luckyConfig;
        [SerializeField] private GameObject _buttonParent;
        
        private string _name;
        private int _level;

        internal void SetUp(string defaultName, Sprite troopIcon, int rarity, int level)
        {
            var translatedName = LeanLocalization.GetTranslationText(defaultName.ToString());
            _name = defaultName;
            _level = level;
            _nameText.text = translatedName;
            _nameText.color = _nameColorRarities[rarity];
            _troopIcon.sprite = troopIcon;
            _bgFrame.sprite = _bgFrameRarities[rarity];
            _bgName.sprite = _bgNameRarities[rarity];
            for (int i = 0; i < level; i++)
            {
                Instantiate(_starPrefab, _starParent);
            }

            _collectButton.InitPrice(_luckyConfig.GetPriceForRarity(rarity));
        }

        internal void Animate()
        {
            _animation.Show();
        }

        internal void ShowPriceAndButton()
        {
            _buttonParent.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _collectButton.OnButtonPressed += HandleOnButtonPressed;
        }

        private void OnDisable()
        {
            _collectButton.OnButtonPressed -= HandleOnButtonPressed;
        }

        private void HandleOnButtonPressed(int price)
        {
            OnLuckyCardPurchased?.Invoke(this, price);
        }
    }

}
