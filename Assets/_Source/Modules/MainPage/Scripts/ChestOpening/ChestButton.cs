using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestButton : MonoBehaviour
    {
        internal Action ChestButtonPressed;

        [SerializeField] private Button _button;
        [SerializeField] private ChestVariableSO[] _chests;
        [SerializeField] private Image _chestImage;
        [SerializeField] private Image _buttonBg;
        [SerializeField] private Sprite _emptyBox;
        [SerializeField] private Sprite _activeButton, _inactiveButton;
        [SerializeField] private Text _nullText;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleOnClick);
            foreach (var chest in _chests)
            {
                chest.ChestQuantityChanged += UpdateAvailability;
            }
            UpdateAvailability();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnClick);
            foreach (var chest in _chests)
            {
                chest.ChestQuantityChanged -= UpdateAvailability;
            }
        }

        internal void HandleOnClick()
        {
            ChestButtonPressed?.Invoke();
        }

        private void UpdateAvailability()
        {
            if (_chests.Any(n => n.QuantityOfChests > 0) == false)
            {
                _button.enabled = false;
                _chestImage.sprite = _emptyBox;
                _buttonBg.sprite = _inactiveButton;
                _nullText.gameObject.SetActive(true);
            }
            else
            {
                _button.enabled = true;
                _buttonBg.sprite = _activeButton;
                _nullText.gameObject.SetActive(false);
                for (int i = 0; i < _chests.Length; i++)
                {
                    if (_chests[i].QuantityOfChests > 0) _chestImage.sprite = _chests[i].ChestIcon;
                }
            }
        }
    }
}
