using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class OfflineTestButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SavedDateTimeVariableSO _offlineTime;
        [SerializeField] private OfflineRewardsDialog _dialog;

        private void OnEnable()
        {
            _button.onClick.AddListener(AddOneHour);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(AddOneHour);
        }

        private void AddOneHour()
        {
            var savedTime = (DateTime)_offlineTime.Value;
            var plusOneHour = savedTime.AddHours(-1);
            _offlineTime.Value = (PersistentData.JsonDateTime)(plusOneHour);
            _dialog.CloseDialog();
        }
    }
}
