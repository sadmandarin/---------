using AutoBattler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class StartBattleButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MenuSwitcher _battleSceneSwitcher;
        [SerializeField] private LevelConfigBaseSO _levelConfig;

        private void OnEnable()
        {
            _button.onClick.AddListener(StartBattle);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(StartBattle);
        }

        internal void StartBattle()
        {
            _battleSceneSwitcher.SwitchToBattle(_levelConfig);
        }
    }
}
