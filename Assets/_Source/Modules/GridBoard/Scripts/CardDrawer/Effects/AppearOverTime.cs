using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class AppearOverTime : MonoBehaviour
    {
        [SerializeField] private float _timeToAppear;
        [SerializeField] private CanvasGroup _canvasGroup;
        private void OnEnable()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, _timeToAppear);
        }
    }
}
