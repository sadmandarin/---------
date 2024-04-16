using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GridBoard
{
    internal class CardAppearingAnimation : MonoBehaviour
    {
        [SerializeField] private float _startY, _endY, _timeToMoveForY;
        [SerializeField] private float _timeToShake, _randomnessOfRotation;
        [SerializeField] private int _vibratoOfRotation;
        [SerializeField] private Vector3 _strengthOfRotation;
        [SerializeField] private float _timeToRotate;
        [SerializeField] private Transform _frontSide;
        [SerializeField] private Transform _backSide;
        [SerializeField] private Transform[] _effects;
        [SerializeField] private float _increaseInSizeDuringRotation, _yIncreaseDuringRotation;
        [SerializeField] private AudioSource _getCardSound;

        internal Sequence Show()
        {
            _backSide.localPosition = new Vector3(_backSide.localPosition.x, _startY, _backSide.localPosition.z);
            return DOTween.Sequence().Append(_backSide.DOLocalMoveY(_endY, _timeToMoveForY))
                              .Append(_backSide.DOShakeRotation(_timeToShake, _strengthOfRotation, _vibratoOfRotation, _randomnessOfRotation
                                                                , false, ShakeRandomnessMode.Harmonic))
                              .Append(_backSide.DORotate(new Vector3(0, 90, 0), _timeToRotate))
                              .Join(_frontSide.DORotate(new Vector3(0, 90, 0), _timeToRotate))
                              .Join(_backSide.DOScale(_increaseInSizeDuringRotation, _timeToRotate * 0.5f).SetRelative(true))
                              .Join(_frontSide.DOScale(_increaseInSizeDuringRotation, _timeToRotate * 0.5f).SetRelative(true))
                              .Join(_backSide.DOLocalMoveY(_yIncreaseDuringRotation, _timeToRotate * 0.5f).SetRelative(true))
                              .Join(_frontSide.DOLocalMoveY(_yIncreaseDuringRotation, _timeToRotate * 0.5f).SetRelative(true))
                              .AppendCallback(SwitchCardSides)
                              .Append(_backSide.DORotate(new Vector3(0, -90, 0), _timeToRotate).SetRelative(true).SetEase(Ease.Linear))
                              .Join(_frontSide.DORotate(new Vector3(0, -90, 0), _timeToRotate).SetRelative(true).SetEase(Ease.Linear))
                              .Join(_frontSide.DOScale(-_increaseInSizeDuringRotation, _timeToRotate * 0.5f).SetRelative(true).SetEase(Ease.InSine))
                              .Join(_backSide.DOScale(-_increaseInSizeDuringRotation, _timeToRotate * 0.5f).SetRelative(true).SetEase(Ease.InSine))
                              .Join(_frontSide.DOLocalMoveY(-_yIncreaseDuringRotation, _timeToRotate * 0.5f).SetRelative(true).SetEase(Ease.InSine))
                              .Join(_backSide.DOLocalMoveY(-_yIncreaseDuringRotation, _timeToRotate * 0.5f).SetRelative(true).SetEase(Ease.InSine))
                              .AppendCallback(TurnOnEffects)
                              .AppendCallback(() => _getCardSound.Play());
        }

        private void TurnOnEffects()
        {
            foreach (var effect in _effects)
            {
                effect.gameObject.SetActive(true);
            }
        }

        private void SwitchCardSides()
        {
            _backSide.gameObject.SetActive(false);
            _frontSide.gameObject.SetActive(true);
        }
    }
}
