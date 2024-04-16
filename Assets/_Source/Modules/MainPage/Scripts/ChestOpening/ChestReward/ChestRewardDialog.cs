using System;
using System.Collections;
using UnityEngine;

namespace MainPage
{
    internal abstract class ChestRewardDialog : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        internal void Init()
        {
            InitCamera();
            SetUp();
        }

        public abstract void SetUp();

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
