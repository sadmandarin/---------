using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class UpdateQuestsTestButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private DailyProgressCollection _progressCollection;
        [SerializeField] private IntVariableSO _progressPoints;

        private void OnEnable()
        {
            _button.onClick.AddListener(UpdateItems);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(UpdateItems);
        }

        private void UpdateItems()
        {
            _questsCompleter.UpdateQuests();
            _progressPoints.Value = 0;
            _progressCollection.InitWithStartingData();
            Destroy(_parentObject);
        }
    }
}
