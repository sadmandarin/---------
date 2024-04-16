using DG.Tweening;
using PersistentData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class LuckyCardDialogCardCollecter : MonoBehaviour
    {
        internal Action OnLastCardCollected;

        [SerializeField] private GameObject _freeSelectButtonParent;
        [SerializeField] private List<LuckyCardFreeCollectButton> _freeCollectButtons;
        [SerializeField] private LightBallTrail _ballTrail;
        [SerializeField] private GameObject _nextButton;
        [SerializeField] private LuckyCardDialogMoreButton _moreButton;
        [SerializeField] private FloatVariableSO _gems;

        private UnitToBoardMover _unitToBoardMover;
        private List<LuckyCardContainer> _cards;

        private void OnEnable()
        {
            foreach (var freeCollectButton in _freeCollectButtons)
            {
                freeCollectButton.OnFreeCardCollected += HandleOnFreeCardCollected;
            }
        }

        private void OnDisable()
        {
            foreach (var freeCollectButton in _freeCollectButtons)
            {
                freeCollectButton.OnFreeCardCollected -= HandleOnFreeCardCollected;
            }
        }

        private void HandleOnFreeCardCollected(int indexOfCard)
        {
            _freeSelectButtonParent.SetActive(false);
            var card = _cards[indexOfCard];
            string cardName = card.Name;
            int cardLevel = card.Level;
            _ballTrail.gameObject.SetActive(true);
            _cards[indexOfCard].gameObject.SetActive(false);
            _unitToBoardMover.MoveUnitToBoardOrBarracks(_ballTrail, cardName, cardLevel)
                .AppendCallback(() => RemoveCardFromList(indexOfCard));
            
        }

        internal void Init(UnitToBoardMover unitToBoardMover, List<LuckyCardContainer> cards)
        {
            _unitToBoardMover = unitToBoardMover;
            _cards = cards;
            foreach (var singleCard in _cards)
            {
                singleCard.OnLuckyCardPurchased += HandleOnLuckyCardPurchased;
            }
            _freeSelectButtonParent.SetActive(true);
        }

        private void RemoveCardFromList(int indexOfCard)
        {
            _ballTrail.gameObject.SetActive(false);
            _cards[indexOfCard].transform.parent.gameObject.SetActive(false);
            Destroy(_cards[indexOfCard].gameObject);
            _cards.Remove(_cards[indexOfCard]);
            if (_cards.Count == 0)
                OnLastCardCollected?.Invoke();
            UnlockCardPricesNew();
        }

        private void UnlockCardPricesNew()
        {
            foreach (var singleCard in _cards)
            {
                singleCard.ShowPriceAndButton();
            }
            _nextButton.SetActive(true);
            _moreButton.gameObject.SetActive(true);
        }

        private void HandleOnLuckyCardPurchased(LuckyCardContainer container, int price)
        {
            var index = _cards.IndexOf(container);
            if (index == -1)
            {
                Debug.LogError("LuckyCardContainer not in list");
                return;
            }
            _gems.Value -= price;
            HandleOnFreeCardCollected(index);
        }
    }
}
