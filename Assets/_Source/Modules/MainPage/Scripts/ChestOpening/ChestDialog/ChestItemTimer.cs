using PersistentData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestItemTimer : MonoBehaviour
    {
        internal Action ChestUnlocked;

        internal int TimeLeft => (int)(_timeLeft - _timer);

        [SerializeField] private Text _timeLeftText;
        //[SerializeField] private string _saveName;


        private int _fullTimeToOpen;
        private bool _initialized, _unlocked;
        private float _timer;
        private DateTime _savedDateTime, _currentDateTime, _targetDateTime;
        private float _timeLeft;
        private SavedDateTimeVariableSO _savedTimeVariable;

        internal void Init(int fullTimeToOpen, SavedDateTimeVariableSO savedTimeVariable)
        {
            _fullTimeToOpen = fullTimeToOpen;
            //_saveName = saveName;
            _savedTimeVariable = savedTimeVariable;
            LoadStartingTime();
            CalculateTimeLeft();
            StartTimeUpdate();
        }

        internal void RemoveOneHourFromTimer()
        {
            _savedDateTime = _savedTimeVariable.Value;

            var savedTime = (DateTime)_savedTimeVariable.Value;
            var plusOneHour = savedTime.AddHours(-1);
            _savedTimeVariable.Value = (PersistentData.JsonDateTime)(plusOneHour);

            LoadStartingTime();
            CalculateTimeLeft();
            StartTimeUpdate();
        }

        private void LoadStartingTime()
        {
            if (_savedTimeVariable.Value.value != 0 && (DateTime)_savedTimeVariable.Value != DateTime.MinValue)
            {
                _savedDateTime = _savedTimeVariable.Value;
                //_savedDateTime = (DateTime)JsonUtility.FromJson<JsonDateTime>(PlayerPrefs.GetString(_saveName, DateTime.UtcNow.ToString()));
            }
            else
            {
                ResetStartingTime();
            }    
        }

        internal void ResetStartingTime()
        {
            //var currentTime = (JsonDateTime)DateTime.UtcNow;
            //PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(currentTime));
            _savedTimeVariable.Value = (PersistentData.JsonDateTime)DateTime.UtcNow;
            _savedDateTime = DateTime.UtcNow;
        }

        internal int CalculateTimeLeft()
        {
            _currentDateTime = DateTime.UtcNow;
            _targetDateTime = _savedDateTime + new TimeSpan(0, 0, 0, _fullTimeToOpen);
            _timeLeft = (float)_targetDateTime.Subtract(_currentDateTime).TotalSeconds;
            return (int)_timeLeft;
        }

        private void StartTimeUpdate()
        {
            _initialized = true;
            _unlocked = false;
        }

        private void Update()
        {
            if (_unlocked)
                return;

            if (_initialized)
            {
                _timer += Time.deltaTime;
                UpdateText();
                if (_timeLeft - _timer <= 0)
                {
                    _unlocked = true;
                    ChestUnlocked?.Invoke();
                }
            }
        }

        private void UpdateText()
        {
            _timeLeftText.text = FormatTime(_timeLeft - _timer);
        }

        private string FormatTime(double timeLeft)
        {
            int hours = (int)(timeLeft / 3600);
            int minutes = (int)(timeLeft % 3600 / 60);
            int seconds = (int)(timeLeft % 60);

            string timeString = $"{hours:00}:{minutes:00}:{seconds:00}";
            return timeString;
        }
    }
}
