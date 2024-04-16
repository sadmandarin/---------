using Lean.Localization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PersistentData
{
    public abstract class DailyActivityBase : MonoBehaviour
    {
        [SerializeField] private SavedDateTimeVariableSO _timeForUpdate;
        [SerializeField] private Text _textToShowTimeLeft;
        [SerializeField] private LeanPhrase _refreshPhrase;

        private double _timeLeft;

        private void Awake()
        {
            CalculateTimeLeft();
        }

        private void CalculateTimeLeft()
        {
            if (_timeForUpdate.Value.value == 0)
                _timeForUpdate.Value = (JsonDateTime)DateTimeHelper.GetNextDay7AM();

            DateTime targetTime = (DateTime)_timeForUpdate.Value;
            if (targetTime < DateTime.UtcNow)
            {
                _timeLeft = -1;
            }
            else
            {
                var timeSpan = targetTime.Subtract(DateTime.UtcNow);
                _timeLeft = timeSpan.TotalSeconds;
            }
            
        }

        private void Update()
        {
            _timeLeft -= Time.deltaTime;
            
            if (_timeLeft < 0 )
            {
                InvokeDailyActivity();
                RefreshTime();
            }

            if (_textToShowTimeLeft != null)
            {
                _textToShowTimeLeft.text = LeanLocalization.GetTranslationText(_refreshPhrase.name) + FormatTime(_timeLeft);
            }
        }

        public abstract void InvokeDailyActivity();

        private void RefreshTime()
        {
            _timeForUpdate.Value = (JsonDateTime)DateTimeHelper.GetNextDay7AM();
            CalculateTimeLeft();
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
