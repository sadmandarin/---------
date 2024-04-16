using Expedition;
using MainPage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legion
{
    public class ExpeditionToMainMenuLink : MonoBehaviour
    {
        [SerializeField] private MoneyCollectorController _moneyCollector;
        [SerializeField] private ItemCollectionAnimation _animation;

        private void OnEnable()
        {
            _moneyCollector.OnMoneyCollected += HandleOnMoneyCollected;
            _moneyCollector.OnGemsCollected += HandleOnGemsCollected;
        }

        private void OnDestroy()
        {
            _moneyCollector.OnMoneyCollected -= HandleOnMoneyCollected;
            _moneyCollector.OnGemsCollected -= HandleOnGemsCollected;
        }

        private void HandleOnMoneyCollected()
        {
            _animation.PlayAnimations(false, true);
        }

        private void HandleOnGemsCollected()
        {
            _animation.PlayAnimations(true, false);
        }
    }
}
