using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class BarracksButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CameraChanger _cameraChanger;
        [SerializeField] private BarracksPage _barracksView;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickHandler);
        }

        private void OnDisable()
        {
            _button?.onClick.RemoveListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            _cameraChanger.ChangeCameraTransform();
            _barracksView.Show();
        }
    }
}
