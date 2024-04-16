using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class CardDrawDialogNextButton : MonoBehaviour
    {
        [SerializeField] private CardDrawCollectButton _collectButton;
        [SerializeField] private Button _button;
        [SerializeField] private CardDrawerCostProgression _costProgression;
        [SerializeField] private Text _costText;
        [SerializeField] private FloatVariableSO _money;
        [SerializeField] private RectTransform _coinParent;
        [SerializeField] private Button _adButton;

        private Action _nextAction;
        private float _price;

        private void OnEnable()
        {
            _button.onClick.AddListener(PurchaseNextCard);
            _adButton.onClick.AddListener(WatchAd);
            UpdateCost();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PurchaseNextCard);
            _adButton.onClick.RemoveListener(WatchAd);
        }

        private void PurchaseNextCard()
        {
            _collectButton.CollectUnit();
            _nextAction.Invoke();
        }

        private void WatchAd()
        {
            YandexManager.Instance.WatchRewardedVideoWithClicker(PurchaseWithAd);
        }

        private void PurchaseWithAd()
        {
            _collectButton.CollectUnit();
            _nextAction.Invoke();
            _money.Value += _price;
        }

        private void UpdateCost()
        {
            var cost = _price;
            _costText.text = cost.ToString();
            var doesPlayerHasEnoughMoney = cost <= _money.Value;
            _button.interactable = doesPlayerHasEnoughMoney;
            _adButton.gameObject.SetActive(doesPlayerHasEnoughMoney == false);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_coinParent);
        }

        internal void Init(Action nextAction, float price)
        {
            _nextAction = nextAction;
            _price = price;

            UpdateCost();
        }
    }
}
