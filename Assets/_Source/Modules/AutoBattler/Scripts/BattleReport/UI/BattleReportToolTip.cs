using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoBattler
{
    public class BattleReportToolTip : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private Image _imageToApplyEffect;
        [SerializeField] private CanvasGroup _toolTipCanvas;

        private void OnClick()
        {
            _toolTipCanvas.alpha = 1;
            if (_imageToApplyEffect.transform.DOComplete() == 0)
                _imageToApplyEffect.transform.DOScale(1.1f, 0.25f).SetLoops(2, LoopType.Yoyo);
        }

        private void OnRemove()
        {
            _toolTipCanvas.alpha = 0;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnRemove();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick();
        }
    }
}
