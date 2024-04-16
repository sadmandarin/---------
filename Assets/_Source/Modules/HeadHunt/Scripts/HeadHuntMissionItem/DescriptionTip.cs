using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeadHunt
{
    internal class DescriptionTip : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleTip);
        }

        private void OnDisable()
        {
            _button?.onClick.RemoveListener(ToggleTip);
        }

        private void ToggleTip()
        {
            _canvasGroup.alpha = _canvasGroup.alpha == 1 ? 0 : 1;
        }
    }
}
