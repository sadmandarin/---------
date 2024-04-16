using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    internal class BoxRewardAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _transformToAnimate;
        [SerializeField] private float _timeToMoveDown = 1f;
        [SerializeField] private float _moveDownY;
        [SerializeField] private float _timeToMoveUp = 1f;

        [ContextMenu(nameof(StartAnimation))]
        internal void StartAnimation()
        {
            _transformToAnimate.SetParent(_target);
            DOTween.Sequence().Append(_transformToAnimate.DOMoveY(-_moveDownY, _timeToMoveDown).SetRelative(true))
                              .Append(_transformToAnimate.DOLocalMove(Vector3.zero, _timeToMoveUp));
        }
    }
}
