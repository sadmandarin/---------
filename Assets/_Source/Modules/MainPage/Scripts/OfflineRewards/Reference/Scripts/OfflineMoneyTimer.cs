using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    public class OfflineMoneyTimer : MoneyTimer
    {
        [SerializeField] private TestDataManager _dataManager;
        [SerializeField] private int _moneyAmount;
        [SerializeField] private Text _moneyText;

        private DateTime _savedDateTime, _currentDateTime, _targetDateTime;
        private float _timeLeft;
        private float _totalTime;
        protected override void ClaimReward()
        {
            // Сюда код для увеличения денег

            base.ClaimReward();
            ResetStartingTime();
            LoadStartingTime();
            CalculateTimeLeft();
        }

        private void Start()
        {
            LoadStartingTime();
            CalculateTimeLeft();
            _moneyText.text = _moneyAmount.ToString();
        }

        private void LoadStartingTime()
        {
            // Нужно как-то загрузить время, когда таймер начался
            // Для времени используется DateTime. Чтобы его сохранить в json нужна обертка. Эта обёртка находится в JsonDateTime
            // Чтобы сохранить - (JsonDateTime)DateTime.Now, загрузить - (DateTime)savedTime
            _savedDateTime = (DateTime)_dataManager.Data.TimerStartTime;
        }

        private void CalculateTimeLeft()
        {
            _currentDateTime = DateTime.UtcNow;
            _targetDateTime = _savedDateTime + new TimeSpan(0, 0, 0, _timeToClaimReward);
            _timeLeft = (float)_targetDateTime.Subtract(_currentDateTime).TotalSeconds;
            _totalTime = _timeToClaimReward;
            _timeToClaimReward = (int)_timeLeft;
        }

        private void ResetStartingTime()
        {
            // Перезагрузка времени когда таймер начался, нужно сохранить это время
            _dataManager.Data.TimerStartTime = (JsonDateTime)DateTime.Now;
            _timeToClaimReward = (int)_totalTime;
            _dataManager.Save();
        }

        protected override void SetFillAndText()
        {
            _fill.fillAmount = 1 - (float)((_timer - _timeLeft + _totalTime) / _totalTime);
            _remainingTimeText.text = FormatTime(_timeLeft - _timer);
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