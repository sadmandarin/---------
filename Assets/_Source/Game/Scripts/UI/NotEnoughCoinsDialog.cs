using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class NotEnoughCoinsDialog : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _button;
        [SerializeField] private VoidEventChannelSO _moveToCoinsEvent;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleOnClick);
            InitCamera();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnClick);
        }

        private void HandleOnClick()
        {
            _moveToCoinsEvent.RaiseEvent();
        }

        private void InitCamera()
        {
            var cameraGameobject = GameObject.FindGameObjectWithTag("CanvasCamera");
            if (cameraGameobject.TryGetComponent(out Camera camera))
            {
                _canvas.worldCamera = camera;
            }

        }
    }
}
