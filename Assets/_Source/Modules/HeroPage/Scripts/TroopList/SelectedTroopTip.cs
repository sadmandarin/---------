using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class SelectedTroopTip : MonoBehaviour
    {
        [SerializeField] private Image _selectedIcon;
        [SerializeField] private Button _selectedButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Vector3 _offset;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        internal void Show(Sprite troopIcon, Transform newParent)
        {
           _selectedIcon.sprite = troopIcon;
            transform.SetParent(newParent);
            transform.transform.localPosition = Vector3.zero + _offset;
            transform.gameObject.SetActive(true);
            transform.localScale = _initialScale;
            transform.DOKill();
            transform.DOScale(_initialScale + new Vector3(0.1f, 0.1f, 0.1f), 0.25f).SetLoops(2, LoopType.Yoyo);
        }

        internal void Hide()
        {
            _canvasGroup.alpha = 1;
            transform.localScale = _initialScale;
        }
    }
}
