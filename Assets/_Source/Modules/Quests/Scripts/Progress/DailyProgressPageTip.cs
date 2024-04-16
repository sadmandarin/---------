using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class DailyProgressPageTip : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _arrowTransform;
        [SerializeField] private Text _tipText, _quantityText;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Vector3 _tipOffset;
        [SerializeField] private float _xOffset;
        [SerializeField] private float _xTrigger;
        [SerializeField] private RectTransform[] _contentParent;

        private bool _isShowing;

        internal void ToggleTipView(bool show)
        {
            _canvasGroup.DOFade(show ? 1 : 0, 0.15f);
            _isShowing = show;
        }

        internal void SetTipPosition(Vector3 position)
        {
            if (position.x >= _xTrigger)
            {
                _rectTransform.position = position + _tipOffset + new Vector3(_xOffset, 0, 0);
                _arrowTransform.position = new Vector2(-_xOffset, _arrowTransform.anchoredPosition.y); 
            }
            else
            {
                _rectTransform.position = position + _tipOffset;
                _arrowTransform.position = new Vector2(0, _arrowTransform.anchoredPosition.y);

            }
        }

        internal void SetTextView(string text, Sprite icon, int quantity)
        {
            _tipText.text = text;
            _rewardImage.sprite = icon;
            _quantityText.text = "x" + quantity.ToString();
            foreach (var content in _contentParent)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            }
        }

        private void Update()
        {
            if (_isShowing == false)
                return;

            if (Input.GetMouseButtonDown(0))
                ToggleTipView(false);
        }
    }
}
