using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    public class MainPageTabButtonUI : MonoBehaviour
    {
        public Action<int> TabSelected;

        [SerializeField] private Image _sprite;
        [SerializeField] private Image _selectedSprite;
        [SerializeField] private int _tabNumber;
        [SerializeField] private Button _tabButton;
        [SerializeField] private Text _text;
        [SerializeField] private Color _selectedTextColor;
        [SerializeField] private Color _unSelectedTextColor;
        [SerializeField] private float _increaseInSize;
        [SerializeField] private float _timeForAnimation = 0.5f;
        [SerializeField] private float _moveUpBy;
        [SerializeField] private Outline _outline;

        private Vector3 _initialSize;
        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialSize = transform.localScale;
            _initialPosition = _sprite.transform.position;
        }

        private void OnEnable()
        {
            _tabButton.onClick.AddListener(SelectTab);
        }

        private void OnDisable()
        {
            _tabButton.onClick.RemoveListener(SelectTab);
        }

        private void SelectTab()
        {
            TabSelected?.Invoke(_tabNumber);
        }

        public void Deselect()
        {
            _text.DOColor(_unSelectedTextColor, _timeForAnimation);
            _sprite.transform.DOScale(_initialSize, _timeForAnimation);
            _text.transform.DOScale(_initialSize, _timeForAnimation);
            _sprite.transform.DOMoveY(_initialPosition.y, _timeForAnimation);
            _selectedSprite.gameObject.SetActive(false);
            _outline.enabled = false;
        }

        public void Select()
        {
            _text.DOColor(_selectedTextColor, _timeForAnimation);
            _sprite.transform.DOScale(_initialSize * _increaseInSize, _timeForAnimation);
            _text.transform.DOScale(_initialSize * _increaseInSize, _timeForAnimation);
            _sprite.transform.DOMoveY(_initialPosition.y + _moveUpBy, _timeForAnimation);
            _selectedSprite.gameObject.SetActive(true);
            _outline.enabled = true;
        }
    }
}