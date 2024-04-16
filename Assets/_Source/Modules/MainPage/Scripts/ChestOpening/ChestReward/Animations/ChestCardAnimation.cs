using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestCardAnimation : MonoBehaviour
    {
        internal Action OnAnimationFinished;

        [SerializeField] private Transform _card;
        [SerializeField] private Transform _cardEffects;
        [SerializeField] private AudioSource _getCardSound;
        [SerializeField] private AudioSource _boxJumpSound;
        [SerializeField] private Image _cardBack;
        [SerializeField] private CanvasGroup _cardCanvasGroup;
        [SerializeField] private ParticleSystem _light;
        [SerializeField] private ParticleSystem _rays;
        [SerializeField] private ParticleSystem _explosion;
        [SerializeField] private float _startingScale;
        [SerializeField] private float _timeToAppear;
        [SerializeField] private float _endScale;
        [SerializeField] private Vector3 _endValueRotation;
        [SerializeField] private float _shrinkRelativeTo;
        [SerializeField] private float _timeToShrink;
        [SerializeField] private float _timeToWaitAfterShrinking;
        [SerializeField] private GameObject _claimButtonParent;

        internal void StartAnimation()
        {
            _cardCanvasGroup.alpha = 1;
            _card.transform.localScale = new Vector3(_startingScale, _startingScale, _startingScale);
            _cardEffects.transform.localScale = new Vector3(_startingScale, _startingScale, _startingScale);
            _light.Play();
            _rays.Play();
            _boxJumpSound.Play();
            DOTween.Sequence().Append(_card.transform.DOScale(_endScale, _timeToAppear))
                              .Join(_cardEffects.transform.DOScale(_endScale, _timeToAppear))
                              .Join(_card.transform.DOLocalRotate(_endValueRotation, _timeToAppear, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear))
                              .Append(_card.transform.DOScale(-_shrinkRelativeTo, _timeToShrink).SetRelative(true))
                              .Join(_cardEffects.transform.DOScale(-_shrinkRelativeTo, _timeToShrink).SetRelative(true))
                              .AppendInterval(_timeToWaitAfterShrinking)
                              .AppendCallback(() =>
                              {
                                  _cardBack.gameObject.SetActive(false);
                                  _explosion.Play();
                                  _getCardSound.Play();
                              })
                              .Append(_card.transform.DOScale(_shrinkRelativeTo, 0).SetRelative(true))
                              .Append(_cardEffects.transform.DOScale(_shrinkRelativeTo, _timeToShrink).SetRelative(true))
                              .AppendCallback(() => _claimButtonParent.SetActive(true))
                              .AppendCallback(() => OnAnimationFinished?.Invoke());



        }
    }
}
