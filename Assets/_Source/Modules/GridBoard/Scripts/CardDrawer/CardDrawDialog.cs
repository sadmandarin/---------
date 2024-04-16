using DG.Tweening;
using Lean.Localization;
using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class CardDrawDialog : MonoBehaviour
    {
        [SerializeField] private SingleCardContainer _singleCardContainer;
        [SerializeField] private CardAppearingAnimation _cardAppearingAnimation;
        [SerializeField] private CardDrawCollectButton _cardDrawCollectButton;
        [SerializeField] private TroopsViewsSO _troopsViews;
        [SerializeField] private GameObject[] _objectToHideDuringAnimation;
        [SerializeField] private ExperienceAdder _experienceAdder;
        [SerializeField] private LuckyMultiplier _luckyMultiplier;
        [SerializeField] private CardDrawDialogNextButton _cardDrawNextButton;
        [SerializeField] private LuckyBonusAnimations _luckyBonusAnimations;
        [SerializeField] private LuckyCardDialogButton _luckyDialogButton;
        [SerializeField] private VoidEventChannelSO _cardAddedToBoard;
        [SerializeField] private LevelVariable _mainLevel;

        internal void Close()
        {
            if (_mainLevel.Value == 2)
                _cardAddedToBoard.RaiseEvent();
            Destroy(gameObject);
        }

        internal void HideUIElements()
        {
            foreach (var item in _objectToHideDuringAnimation)
            {
                item.SetActive(false);
            }
        }

        internal void Init(string troopName, Action drawAction, UnitToBoardMover unitToBoardMover, 
                           float price, int levelOfUnit = 1)
        {
            var troopData = _troopsViews.GetTroopViewByName(troopName);
            _singleCardContainer.SetUp(LeanLocalization.GetTranslationText(troopData.Name), troopData.Icon, troopData.rarity, levelOfUnit);
            _luckyMultiplier.AddLuckyBonusAndSetUp(levelOfUnit);
            Sequence sequence = _cardAppearingAnimation.Show();
            sequence.Append(_luckyBonusAnimations.AnimateLuckyBonus());
            _cardDrawCollectButton.Init(this, troopData.Name, levelOfUnit, unitToBoardMover);
            if (_mainLevel.Value == 2)
                _cardDrawNextButton.gameObject.SetActive(false);
            else
                _cardDrawNextButton.Init(drawAction, price);
            _luckyDialogButton.Init(unitToBoardMover);
            _experienceAdder.AddExperienceForBuyingTroops(troopData.rarity, levelOfUnit - 1);
        }
    }
}
