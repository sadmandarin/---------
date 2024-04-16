using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    internal class NotEnoughResourcesDialog : MonoBehaviour
    {
        internal Action OnBuyMorePressed;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleOnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnClick);
        }

        private void HandleOnClick()
        {
            OnBuyMorePressed?.Invoke();
        }

        internal void InitCamera(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }
    }
}
