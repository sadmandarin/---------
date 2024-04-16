using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;
using PersistentData;
using System;

namespace GridBoard
{
    internal class CardDrawButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _costText;
        [SerializeField] private GameObject _redDot;
        [SerializeField] private CardDrawDialog _dialog;
        [SerializeField] private CardDrawerSelectDialog _selectDialog;
        [SerializeField] private CardDrawerConfig _cardDrawerConfig;
        [SerializeField] private LevelVariable _level;
        [SerializeField] private Camera _canvasCamera;
        [SerializeField] private Transform _parentToSpawnDialog;
        [SerializeField] private UnitToBoardMover _unitToBoardMover;
        [SerializeField] private CardDrawerCostProgression _costProgression;
        [SerializeField] private FloatVariableSO _money;
        [SerializeField] private RectTransform _textParent;
        [SerializeField] private Color _activeColor, _inactiveColor;
        [SerializeField] private GameObject _notEnoughCoinsDialog;

        private bool _doesPlayerHasEnoughMoney;

        internal void ShowDialog()
        {
            if (_doesPlayerHasEnoughMoney)
            {
                if (_costProgression.ReachedLimit() == false)
                    SpawnNewCardDialog();
                else
                    SpawnCardSelectDialog();
            }
            else
            {
                var dialog = Instantiate(_notEnoughCoinsDialog);
            }
        }

        private void SpawnCardSelectDialog()
        {
            var dialog = Instantiate(_selectDialog);
            dialog.Init(_unitToBoardMover, _parentToSpawnDialog);
        }

        private void SpawnNewCardDialog()
        {
            var cost = _costProgression.GetCost();
            _money.Value -= cost;
            _costProgression.IncreaseIndex();
            var newCost = _costProgression.GetCost();
            var dialog = Instantiate(_dialog, _parentToSpawnDialog);
            dialog.Init(_cardDrawerConfig.GetRandomUnitForDrawer(_level.Value), SpawnNewCardDialog, _unitToBoardMover, newCost, 1);
            
            SetUpAvailability();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ShowDialog);
            SetUpAvailability();
            
            _money.OnValueChanged += SetUpAvailability;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowDialog);

            _money.OnValueChanged -= SetUpAvailability;
        }

        private void SetUpAvailability(float price = 0)
        {
            var cost = _costProgression.GetCost();
            _costText.text = cost.ToString();
            _doesPlayerHasEnoughMoney = cost <= _money.Value;
            _costText.color = _doesPlayerHasEnoughMoney ? _activeColor : _inactiveColor;
            _redDot.SetActive(_doesPlayerHasEnoughMoney);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_textParent);
        }
    }
}
