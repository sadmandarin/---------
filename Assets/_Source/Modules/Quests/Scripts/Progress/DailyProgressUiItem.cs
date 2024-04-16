using DG.Tweening;
using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class DailyProgressUiItem : MonoBehaviour
    {
        internal Action<DailyProgressItemSO, DailyProgressUiItem> OnTryingToClaim, OnRequestingTip;

        [SerializeField] private Image _boxImage;
        [SerializeField] private Text _progressPointsText;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private IntVariableSO _progressPoints;
        [SerializeField] private DailyProgressBoxAnimation _boxAnimation;
        [SerializeField] private Button _button;
        
        private DailyProgressItemSO _itemData;
        private bool _isCollected;
        private bool HasEnoughPoints => _progressPoints.Value >= _itemData.ProgressPointsToUnlock;

        internal RectTransform RectTransform => _rectTransform;

        internal void SetUp(DailyProgressItemSO itemData, bool isCollected)
        {
            _itemData = itemData;
            _isCollected = isCollected;

            _boxImage.sprite = itemData.LockedSprite;
            _progressPointsText.text = itemData.ProgressPointsToUnlock.ToString();

            UpdateBoxView();
        }

        internal void SetXPosition(float xPosition)
        {
            _rectTransform.anchoredPosition = new Vector2(xPosition, _rectTransform.anchoredPosition.y);
        }

        private void OnEnable()
        {
            _progressPoints.OnValueChanged += UpdateBoxView;
            _button.onClick.AddListener(RaiseEvent);
        }

        private void OnDisable()
        {
            _progressPoints.OnValueChanged -= UpdateBoxView;
            _button.onClick.RemoveListener(RaiseEvent);
        }

        private void UpdateBoxView(int points = 0)
        {
            if (_isCollected)
            {
                _boxImage.sprite = _itemData.UnlockedSprite;
                _boxAnimation.StopAnimation();
                return;
            }

            if (HasEnoughPoints)
            {
                _boxAnimation.StartAnimation();
                _boxImage.sprite = _itemData.LockedSprite;
            }
            else
            {
                _boxImage.sprite = _itemData.LockedSprite;
                _boxAnimation.StopAnimation();
            }
                
        }

        private void RaiseEvent()
        {
            if (HasEnoughPoints && _isCollected == false)
                OnTryingToClaim?.Invoke(_itemData, this);
            else
            {
                OnRequestingTip?.Invoke(_itemData, this);
                BounceAnimation();
            }
                
        }

        private void BounceAnimation()
        {
            _boxImage.transform.DOScale(1.1f, 0.15f).SetLoops(2, LoopType.Yoyo);
        }
    }

}
