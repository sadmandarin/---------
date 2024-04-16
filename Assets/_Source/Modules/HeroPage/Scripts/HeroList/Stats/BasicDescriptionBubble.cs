using System;
using UnityEngine;

namespace HeroPage
{
    internal class BasicDescriptionBubble : MonoBehaviour, ITipBubble
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public Action<ITipBubble> TipShown { get; set; }

        public bool IsActive { get; private set; }

        public void Hide()
        {
            IsActive = false;
            _canvasGroup.alpha = 0;
        }

        public void Show()
        {
            IsActive = true;
            _canvasGroup.alpha = 1;
            TipShown?.Invoke(this);
        }
    }
}
