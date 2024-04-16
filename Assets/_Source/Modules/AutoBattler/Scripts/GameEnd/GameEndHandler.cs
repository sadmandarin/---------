using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class GameEndHandler : MonoBehaviour
    {
        [SerializeField] private AutoBattlerRoot _battlerRoot;

        private void OnEnable()
        {
            _battlerRoot.OnPlayerWon += OnPlayerWon;
            _battlerRoot.OnPlayerLost += OnPlayerLost;
        }

        private void OnDisable()
        {
            _battlerRoot.OnPlayerWon -= OnPlayerWon;
            _battlerRoot.OnPlayerLost -= OnPlayerLost;
        }

        private void OnPlayerLost()
        {
            throw new NotImplementedException();
        }

        private void OnPlayerWon()
        {
            throw new NotImplementedException();
        }
    }
}
