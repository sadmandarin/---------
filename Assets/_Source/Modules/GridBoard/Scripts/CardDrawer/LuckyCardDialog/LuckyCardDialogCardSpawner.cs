using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnitsData;
using UnityEngine;

namespace GridBoard
{
    internal class LuckyCardDialogCardSpawner : MonoBehaviour
    {
        internal Action OnCardsSpawned;

        [SerializeField] private LuckyCardContainer _cardContainerPrefab;
        [SerializeField] private Transform[] _cardPositions;
        [SerializeField] private LuckyConfigSO _luckyConfig;
        [SerializeField] private CardDrawerConfig _cardDrawerConfig;
        [SerializeField] private float _delayBetweenCards = 0.3f;

        private List<LuckyCardContainer> _luckyCardContainers = new List<LuckyCardContainer>();

        internal List<LuckyCardContainer> SpawnCards()
        {
            _luckyCardContainers = new List<LuckyCardContainer>();
            var threeRandomCards = _cardDrawerConfig.GetRandomUnitsForLuckyDialog();
            _luckyConfig.ReduceLuckyBonus();
            int levelOfUnits = 2;
            StartCoroutine(AnimateCards(threeRandomCards, levelOfUnits));
            return _luckyCardContainers;
        }

        private IEnumerator AnimateCards(List<UnitPurchaseDataSO> cards, int levelOfUnits)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                var card = Instantiate(_cardContainerPrefab, _cardPositions[i]);
                var cardData = cards[i].UnitView;
                card.SetUp(cardData.Name.ToString(), cardData.Icon, cardData.Rarity, levelOfUnits);
                card.Animate();
                _luckyCardContainers.Add(card);
                yield return new WaitForSeconds(_delayBetweenCards);
            }

            OnCardsSpawned?.Invoke();
        }
    }
}
