using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class RotateOverTime : MonoBehaviour
    {
        [SerializeField] private float _timeForOneSpin;
        [SerializeField] private Vector3 _rotationAfterOneSpin = new Vector3(0,0,360);
        private void Start()
        {
            transform.DOLocalRotate(_rotationAfterOneSpin, _timeForOneSpin, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear)
                .SetLoops(-1);
        }
        
    }
}
