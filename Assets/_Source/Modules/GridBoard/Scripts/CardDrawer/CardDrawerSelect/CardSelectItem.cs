using System;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class CardSelectItem : MonoBehaviour
    {
        internal Action<CardDrawerOdds> OnCardSelected;
        internal CardDrawerOdds Odds => _cardDrawerOdds;

        [SerializeField] private Image _cardbackImage;
        [SerializeField] private Text _starsCount;
        [SerializeField] private Button _button;

        private CardDrawerOdds _cardDrawerOdds;

        internal void Init(CardDrawerOdds cardDrawerOdds)
        {
            _cardDrawerOdds = cardDrawerOdds;
            _cardbackImage.sprite = cardDrawerOdds.CardBackIcon;
            _starsCount.text = cardDrawerOdds.LevelOfUnit.ToString();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SelectCard);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SelectCard);
        }

        private void SelectCard()
        {
            OnCardSelected?.Invoke(_cardDrawerOdds);
        }
    }
}
