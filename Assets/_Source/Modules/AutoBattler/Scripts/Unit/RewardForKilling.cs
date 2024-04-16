using System;
using UnityEngine;

namespace AutoBattler
{
    internal class RewardForKilling : MonoBehaviour
    {
        internal int Reward => _reward;

        [SerializeField] private int _reward = 10;

        internal void ChangeRewardBy(int increment)
        {
            _reward += increment;
        }

        internal void SetValue(int value)
        {
            if (value <= 10)
            {
                _reward = 10;
                return;
            }

            _reward = value;
        }
    }
}
