using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public class DeadCoin : MonoBehaviour
    {
        [SerializeField] private TextMesh _numberText;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private SpriteRenderer _coin;
        [SerializeField] private float _timeToFade;
        [SerializeField] private float _moveYby;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        internal void Init(int numberOfCoins)
        {
            _numberText.text = "+" + numberOfCoins;
            transform.position += _offset;
            StartFading();
        }

        private void StartFading()
        {
            Color oldColor = _numberText.color;
            DOTween.Sequence().Append(_coin.DOFade(0, _timeToFade))
                              .Join(DOTween.ToAlpha(() => _numberText.color, x => _numberText.color = x, 0, _timeToFade))
                              .Join(transform.DOMoveY(_moveYby, _timeToFade).SetRelative(true))
                              .AppendCallback(() => Destroy(gameObject));
        }

        private void Update()
        {
            transform.rotation = _camera.transform.rotation;
        }
    }
}
