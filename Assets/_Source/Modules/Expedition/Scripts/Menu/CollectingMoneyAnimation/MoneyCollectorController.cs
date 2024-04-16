using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class MoneyCollectorController : MonoBehaviour
    {
        public Action OnMoneyCollected, OnGemsCollected;

        [SerializeField] private CanvasGroup _currencyBar;
        [SerializeField] private float _timeToShowCurrencyBar;

        private const string CollectMoneySave = "CollectMoney";

        internal void CollectMoney()
        {
            PlayerPrefs.SetInt(CollectMoneySave, 0);
            OnMoneyCollected?.Invoke();
            StartCoroutine(ShowCurrencyBar());
        }

        internal void CollectGems()
        {
            OnGemsCollected?.Invoke();
            StartCoroutine(ShowCurrencyBar());
        }

        private void OnEnable()
        {
            if (PlayerPrefs.GetInt(CollectMoneySave, 0) == 0)
                return;

            CollectMoney();
        }

        private IEnumerator ShowCurrencyBar()
        {
            _currencyBar.alpha = 1;
            yield return new WaitForSeconds(_timeToShowCurrencyBar);
            _currencyBar.alpha = 0;
        }
    }
}
