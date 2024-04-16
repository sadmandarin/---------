using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    internal class ExtraRewardAnimation : MonoBehaviour
    {
        [SerializeField] private float _amountToMoveY;
        [SerializeField] private float _timeForOneLoop;

        private Vector3 _initialPosition;

        private void OnEnable()
        {
            
        }
        private void Awake()
        {
            _initialPosition = transform.position;
            transform.position = _initialPosition;
            transform.DOLocalMoveY(transform.localPosition.y + _amountToMoveY, _timeForOneLoop).SetLoops(-1, LoopType.Yoyo);
            transform.position = _initialPosition;
        }
        private void OnDisable()
        {
            transform.DOPause();
        }
    }
}
