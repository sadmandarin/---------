using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class CardDrawerSelectDialog : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<CardDrawerOdds> _oddsList;
        [SerializeField] private CardSelectItem _selectItemPrefab;
        [SerializeField] private Transform _parentForCards;
        [SerializeField] private Text _commonProbability, _rareProbability, _price;
        [SerializeField] private LuckyCardDialogButton _luckyButton;
        [SerializeField] private CardDrawerSelectPurchaseButton _purchaseButton;
        [SerializeField] private CardSelectScrollRectSnap _scrollSnap;

        private UnitToBoardMover _unitToBoardMover;
        private List<CardSelectItem> _cards = new List<CardSelectItem>();

        private void OnEnable()
        {
            InitCamera();
            InitCards();

            _purchaseButton.OnDialogSpawned += CloseDialog;
        }

        private void OnDisable()
        {
            _purchaseButton.OnDialogSpawned -= CloseDialog;
        }

        private void CloseDialog()
        {
            Destroy(gameObject);
        }

        private void InitCamera()
        {
            var cameraGameobject = GameObject.FindGameObjectWithTag("CanvasCamera");
            if (cameraGameobject.TryGetComponent(out Camera camera))
            {
                _canvas.worldCamera = camera;
            }
        }


        private void InitCards()
        {
            foreach (var odds in _oddsList)
            {
                var card = Instantiate(_selectItemPrefab, _parentForCards);
                card.Init(odds);
                card.OnCardSelected += HandleCardSelected;
                _cards.Add(card);
            }
            SelectCard(0);
        }

        private void HandleCardSelected(CardDrawerOdds odds)
        {
            _commonProbability.text = (odds.CommonOdds * 100) + "%";
            _rareProbability.text = (odds.RareOdds * 100) + "%";

            _purchaseButton.UpdatePrice(odds.Price, odds);
            _scrollSnap.SnapToElement(_cards.FindIndex(n => n.Odds == odds));
        }

        internal void Init(UnitToBoardMover unitToBoardMover, Transform parentToSpawnDialog)
        {
            _unitToBoardMover = unitToBoardMover;
            _luckyButton.Init(unitToBoardMover);
            _purchaseButton.Init(unitToBoardMover, parentToSpawnDialog);
        }

        internal void SelectCard(int index)
        {
            var odds = _cards[index].Odds;
            HandleCardSelected(odds);
        }
    }
}
