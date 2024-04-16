using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ShopPage;

namespace Legion
{
    internal class ButtonThatLoadsResourcesPage : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MenuSwitcher _menuSwitcher;
        [SerializeField] private bool _loadCoins;

        private void OnEnable()
        {
            _button.onClick.AddListener(SwitchView);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SwitchView);
        }

        private void SwitchView()
        {
            _menuSwitcher.SwitchViewToResoucesPage(_loadCoins);
        }
    }
}
