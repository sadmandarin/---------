using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    public class IncreaseAndDecreaseInSize : MonoBehaviour
    {
        [SerializeField] private float _targetScale;
        [SerializeField] private float _timeForOneLoop;

        private float _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale.x;
        }

        private void OnEnable()
        {
            transform.DOScale(_targetScale, _timeForOneLoop * 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}
