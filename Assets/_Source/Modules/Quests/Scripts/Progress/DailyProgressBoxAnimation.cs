using DG.Tweening;
using UnityEngine;

namespace Quests
{
    internal class DailyProgressBoxAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _boxImage;
        [SerializeField] private Vector3 _leftRotation, _rightRotation;
        [SerializeField] private float _timeToRotateToLeft, _timeToRotateToRight, _timeToWait;
        [SerializeField] private GameObject _lightEffect;
        
        private Sequence _sequence;

        internal void StartAnimation()
        {
            _lightEffect.SetActive(true);

            _sequence = DOTween.Sequence().Append(_boxImage.DOLocalRotate(_leftRotation, _timeToRotateToLeft))
                                          .Append(_boxImage.DOLocalRotate(_rightRotation, _timeToRotateToRight).SetLoops(10,LoopType.Yoyo))
                                          .Append(_boxImage.DOLocalRotate(Vector3.zero, _timeToRotateToLeft))
                                          .AppendInterval(_timeToWait)
                                          .SetLoops(-1);
        }

        internal void StopAnimation()
        {
            _lightEffect.SetActive(false);
            _boxImage.DOKill();
            if (_sequence != null)
                _sequence.Kill();
            _boxImage.DOLocalRotate(Vector3.zero, 0);
        }
    }

}
