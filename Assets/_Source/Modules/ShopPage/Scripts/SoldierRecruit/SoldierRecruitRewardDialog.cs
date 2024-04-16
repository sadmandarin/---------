using Lean.Localization;
using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    internal class SoldierRecruitRewardDialog : MonoBehaviour
    {
        [SerializeField] private Transform _cardsParent;
        [SerializeField] private Button _claimButton;
        [SerializeField] private SoldierRecruitCard _cardPrefab;
        [SerializeField] private float _delayBetweenCards;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private SoldierRecruitConfig _soldierRecruitConfig;
        [SerializeField] private ScrollRect _scrollRect;

        internal void SpawnCards(int numberOfCards, int level)
        {
            StartCoroutine(SpawnCardCoroutine(numberOfCards, level));
        }

        internal void InitCamera(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }

        private void OnEnable()
        {
            _claimButton.onClick.AddListener(HandleClaimed);
            
        }

        private void OnDisable()
        {
            _claimButton.onClick.RemoveListener(HandleClaimed);
        }

        private IEnumerator SpawnCardCoroutine(int numberOfCards, int level)
        {
            List<UnitViewSO> cardsToGet = _soldierRecruitConfig.AddNewTroops(numberOfCards, level);

            for (int i = 0; i < numberOfCards; i++)
            {
                var randomTroop = cardsToGet[i];
                var card = Instantiate(_cardPrefab, _cardsParent);
                var troopName = randomTroop.Name.ToString();
                card.SetUp(LeanLocalization.GetTranslationText(troopName), randomTroop.Icon, randomTroop.Rarity, level);
                LayoutRebuilder.ForceRebuildLayoutImmediate(_cardsParent.GetComponent<RectTransform>());
                ScrollViewFocusFunctions.FocusOnItem(_scrollRect, card.GetComponent<RectTransform>());
                yield return new WaitForSeconds(_delayBetweenCards);
                
            }
        }

        private void HandleClaimed()
        {
            Destroy(gameObject);
        }
    }
}
