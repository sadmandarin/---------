using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YandexSDK;

namespace Legion
{
    public class PauseDialog : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _continueButton;

        public Action OnHomePressed { get; internal set; }

        internal void InitCamera(Camera worldCamera)
        {
            _canvas.worldCamera = worldCamera;
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(InvokeOnHomePressed);
            PauseGame();
            ApplicationActivation.Enabled = false;
        }

        private void InvokeOnHomePressed()
        {
            OnHomePressed?.Invoke();
            Destroy(gameObject);
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(InvokeOnHomePressed);
            Time.timeScale = 1;
            AudioListener.pause = false;
            ApplicationActivation.Enabled = true;
        }
    }
}
