using DG.Tweening;
using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class TaskNoticeView : MonoBehaviour
    {
        [SerializeField] private Text _titleText;
        [SerializeField] private Image _progressImage;
        [SerializeField] private GameObject _progressNotComplete;
        [SerializeField] private GameObject _progressComplete;
        [SerializeField] private ParticleSystem _completeEffect;
        [SerializeField] private Text _progressText;
        [SerializeField] private RectTransform _transformToMove;
        [SerializeField] private float _timeToShowPanel;
        [SerializeField] private float _timeToFill;
        [SerializeField] private float _timeAfterFill;
        [SerializeField] private float _timeToHidePanel;
        [SerializeField] private float _yWhenHidden;
        [SerializeField] private float _yWhenShown;

        private float _ratioOfCompletion;

        internal void SetUp(int timesCompleted, int completionRequirement, string title)
        {
            _ratioOfCompletion = (float)timesCompleted / (float)completionRequirement;
            _progressText.text = $"{timesCompleted}/{completionRequirement}";
            _titleText.text = title;
        }

        internal void DoAnimation()
        {
            ResetVisual();
            
            DOTween.Sequence().Append(_transformToMove.DOAnchorPosY(_yWhenShown, _timeToShowPanel))
                              .Append(_progressImage.DOFillAmount(_ratioOfCompletion, _timeToFill))
                              .AppendCallback(AnimateEffectWhenComplete)
                              .AppendInterval(_timeAfterFill)
                              .Append(_transformToMove.DOAnchorPosY(_yWhenHidden, _timeToHidePanel));
        }

        private void AnimateEffectWhenComplete()
        {
            if (_ratioOfCompletion >= 1)
            {
                _progressNotComplete.SetActive(false);
                _progressComplete.SetActive(true);
                _completeEffect.Play();
            }
            else
                return;
        }

        internal void ResetVisual()
        {
            _progressNotComplete.SetActive(true);
            _progressComplete.SetActive(false);
            _progressImage.fillAmount = 0;
            _transformToMove.DOAnchorPosY(_yWhenHidden, 0);
            //_transformToMove.DOMoveY(_yWhenHidden, 0);
        }
    }
}
