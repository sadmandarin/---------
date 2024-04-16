using System;
using UnityEngine;

namespace HeroPage
{
    internal class HeroOriginDialog : MonoBehaviour
    {
        internal Action OnMovingEvent;

        [SerializeField] private Canvas _canvas;
        
        private GameObject _heroCamera;

        private void OnDisable()
        {
            _heroCamera.SetActive(true);
        }

        internal void InitCamera(Camera canvasCamera, GameObject heroCamera)
        {
            _canvas.worldCamera = canvasCamera;
            _heroCamera = heroCamera;
            _heroCamera.SetActive(false);
        }

        internal void RaiseOnMovingEvent()
        {
            OnMovingEvent?.Invoke();
        }
    }
}
