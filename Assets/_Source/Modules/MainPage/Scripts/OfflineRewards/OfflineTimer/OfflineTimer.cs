using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    internal class OfflineTimer : MonoBehaviour
    {
        [SerializeField] private int _timeToClaimReward;
        [SerializeField] private bool _loadTime;
        [SerializeField] private SavedDateTimeVariableSO _savedOfflineIncomeTime;

        private DateTime _savedDateTime, _currentDateTime, _targetDateTime;
        private float _timeLeft;
        private float _totalTime;

        private void Awake()
        {
            UpdateTime();
        }

        internal void UpdateTime()
        {
            if (_loadTime)
                LoadStartingTime();
            else
                ResetStartingTime();
            CalculateTimeLeft();
        }

        internal void ResetStartingTime()
        {
            _savedOfflineIncomeTime.Value = (PersistentData.JsonDateTime)DateTime.UtcNow;
            //PlayerPrefs.SetString("TimerStartTime", JsonUtility.ToJson(currentTime));
            _savedDateTime = DateTime.UtcNow;
        }

        private void LoadStartingTime()
        {
            // Нужно как-то загрузить время, когда таймер начался
            // Для времени используется DateTime. Чтобы его сохранить в json нужна обертка. Эта обёртка находится в JsonDateTime
            // Чтобы сохранить - (JsonDateTime)DateTime.Now, загрузить - (DateTime)savedTime

            //_savedDateTime = (DateTime)JsonUtility.FromJson<JsonDateTime>(PlayerPrefs.GetString("TimerStartTime"));
            if (_savedOfflineIncomeTime.Value.value == 0)
                _savedOfflineIncomeTime.Value = (PersistentData.JsonDateTime)DateTime.UtcNow;
            _savedDateTime = (DateTime)_savedOfflineIncomeTime.Value;
        }

        internal int CalculateTimeLeft()
        {
            _currentDateTime = DateTime.UtcNow;
            _targetDateTime = _savedDateTime + new TimeSpan(0, 0, 0, _timeToClaimReward);
            _timeLeft = (float)_targetDateTime.Subtract(_currentDateTime).TotalSeconds;
            _totalTime = _timeToClaimReward;
            return (int)_timeLeft;
        }
    }
}
