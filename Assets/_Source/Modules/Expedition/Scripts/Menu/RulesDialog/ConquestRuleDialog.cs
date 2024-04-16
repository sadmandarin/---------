using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestRuleDialog : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _button;

        internal void Init(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
            _canvas.sortingLayerID = 0;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(CloseDialog);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(CloseDialog);
        }

        private void CloseDialog()
        {
            Destroy(gameObject);
        }
    }
}
