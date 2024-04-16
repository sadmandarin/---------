using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MysticStore
{
    internal class MysticStoreRefreshButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MysticStoreItemUpdater _itemUpdater;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private float _price;

        private void OnEnable()
        {
            _button.onClick.AddListener(RefreshItems);
            UpdateButtonAvailability();
            _gems.OnValueChanged += UpdateButtonAvailability;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RefreshItems);
            _gems.OnValueChanged -= UpdateButtonAvailability;
        }

        private void RefreshItems()
        {
            _itemUpdater.UpdateItems();
            _gems.Value -= _price;
        }

        private void UpdateButtonAvailability(float gems = 0)
        {
            _button.interactable = _gems.Value >= _price;
        }
    }
}
