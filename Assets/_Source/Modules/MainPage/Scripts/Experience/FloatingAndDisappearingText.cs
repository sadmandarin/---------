﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class FloatingAndDisappearingText : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private float _timeToFloat;
        [SerializeField] private float _amountToMoveUp;

        private void OnEnable()
        {
            _text.transform.DOLocalMoveY(_amountToMoveUp, _timeToFloat * 0.6f).SetRelative(true);
            _text.DOFade(0, _timeToFloat).OnComplete(() => Destroy(gameObject));
        }

        internal void SetText(string text)
        {
            _text.text = text;
        }
    }
}
