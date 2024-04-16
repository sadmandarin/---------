using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class DoubleSpeedButton : MonoBehaviour
    {
        [SerializeField] private Button _normalButton;
        [SerializeField] private Button _doubleSpeedButton;
        [SerializeField] private Button _notPurchasedButton;
        [SerializeField] private GameObject _tip;
        [SerializeField] private Text _doubleSpeedText;
        [SerializeField] private BoolVariableSO _doubleSpeedVariable;

        private const string PlayerPrefsSave = "DoubleSpeed";

        private int _doubleSpeed;
        private bool _isDoubleSpeed => _doubleSpeed == 1;

        private void OnEnable()
        {
            Init();
            _normalButton.onClick.AddListener(ToggleSpeed);
            _doubleSpeedButton.onClick.AddListener(ToggleSpeed);
            _notPurchasedButton.onClick.AddListener(ToggleTip);
        }

        internal void Init()
        {
            _doubleSpeed = PlayerPrefs.GetInt(PlayerPrefsSave, 0);
            SwitchView(_isDoubleSpeed);
            SwitchSpeed(_isDoubleSpeed);

            
        }

        private void ToggleTip()
        {
            _tip.SetActive(!_tip.activeInHierarchy);
        }

        private void OnDisable()
        {
            _normalButton.onClick.RemoveListener(ToggleSpeed);
            _doubleSpeedButton.onClick.RemoveListener(ToggleSpeed);
            _notPurchasedButton.onClick.RemoveListener(ToggleTip);
        }

        private void SwitchView(bool isDoubleSpeed)
        {
            if (_doubleSpeedVariable.Value == false)
            {
                _notPurchasedButton.gameObject.SetActive(true);
                _normalButton.gameObject.SetActive(false);
                _doubleSpeedButton.gameObject.SetActive(false);
                _doubleSpeedText.gameObject.SetActive(false);
                SwitchSpeed(false);
            }
            else
            {
                _notPurchasedButton.gameObject.SetActive(false);
                _normalButton.gameObject.SetActive(isDoubleSpeed == false);
                _doubleSpeedText.gameObject.SetActive(true);
                _doubleSpeedButton.gameObject.SetActive(isDoubleSpeed);
                _doubleSpeedText.text = isDoubleSpeed ? "x2" : "x1";
            }
        }

        private void SwitchSpeed(bool isDoubleSpeed)
        {
            Time.timeScale = isDoubleSpeed ? 2 : 1;
        }

        private void ToggleSpeed()
        {
            if (_doubleSpeedVariable.Value == false)
            {
                return;
            }
            else
            {
                _doubleSpeed = _doubleSpeed == 0 ? 1 : 0;
                PlayerPrefs.SetInt(PlayerPrefsSave, _doubleSpeed);
                SwitchSpeed(_isDoubleSpeed);
                SwitchView(_isDoubleSpeed);
            }
        }
    }
}
