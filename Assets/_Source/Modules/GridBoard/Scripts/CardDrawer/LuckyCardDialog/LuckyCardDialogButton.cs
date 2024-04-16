using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class LuckyCardDialogButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private LuckyCardDialog _dialog;
        [SerializeField] private CardDrawCollectButton _collect;
        [SerializeField] private LuckyConfigSO _config;
        [SerializeField] private IntVariableSO _lucky;

        private UnitToBoardMover _unitToBoardMover;

        internal void Init(UnitToBoardMover unitToBoardMover)
        {
            _unitToBoardMover = unitToBoardMover;
        }


        private void OnEnable()
        {
            _button.onClick.AddListener(SpawnDialog);
            _lucky.OnValueChanged += UpdateAvailability;
            UpdateAvailability();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SpawnDialog);
            _lucky.OnValueChanged -= UpdateAvailability;
        }

        private void SpawnDialog()
        {
            var dialog = Instantiate(_dialog);
            dialog.Init(_unitToBoardMover);
            dialog.OnSpawnAnotherDialog += SpawnDialog;
            if (_collect != null)
                _collect.CollectUnit();
        }

        private void UpdateAvailability(int value = 0)
        {
            _button.enabled = _config.IsProgressFull;
        }
    }
}
