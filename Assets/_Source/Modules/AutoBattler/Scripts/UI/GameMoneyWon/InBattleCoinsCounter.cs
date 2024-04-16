using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class InBattleCoinsCounter : MonoBehaviour
    {
        internal int CoinsReward => _coinsReward;

        [SerializeField] private Text _counterText;

        private int _coinsReward = 0;

        internal void IncreaseCounter(int amount)
        {
            _coinsReward += amount;
            _counterText.text = _coinsReward.ToString();
        }

        internal void ResetCoins()
        {
            _coinsReward = 0;
            _counterText.text = _coinsReward.ToString();
        }
    }
}
