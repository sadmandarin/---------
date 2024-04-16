using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestRewardLevel : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Transform _normalRewardPosition;
        [SerializeField] private Transform _vipRewardPosition;
        [SerializeField] private Text[] _starsToGetText;
        [SerializeField] private Image _progressLeft, _progressRight;
        [SerializeField] private GameObject _finished, _notFinished;

        public RectTransform RectTransform { get => _rectTransform; private set => _rectTransform = value; }

        internal void SetUp(ConquestRewardItem normalItem, ConquestRewardItem vipItem, int starsToGet, int starsCollected)
        {
            normalItem.transform.SetParent(_normalRewardPosition, false);
            vipItem.transform.SetParent(_vipRewardPosition, false);
            FillProgress(starsToGet, starsCollected);
            foreach(var text in _starsToGetText)
                text.text = starsToGet.ToString();
        }

        private void FillProgress(int starsToGet, int starsCollected)
        {
            float margin = 2.5f;
            if (starsToGet == 3 || starsToGet == 5)
                margin = 1.5f;
            var leftBorder = starsToGet - margin;
            var rightBorder = starsToGet + margin;
            if (starsToGet == 5)
            {
                leftBorder = 4;
                rightBorder = 7.5f;
            }    
            var leftProgress = Mathf.Clamp((float)(starsCollected - leftBorder) / (float)(starsToGet - leftBorder), 0, 1);
            var rightProgress = Mathf.Clamp((float)(starsCollected - starsToGet) / (float)(rightBorder - starsToGet), 0, 1);

            SetTextColor(leftProgress == 1);

            _progressLeft.fillAmount = leftProgress;
            _progressRight.fillAmount = rightProgress;
        }

        private void SetTextColor(bool isFinished)
        {
            _finished.SetActive(isFinished);
            _notFinished.SetActive(isFinished == false);
        }
    }
}
