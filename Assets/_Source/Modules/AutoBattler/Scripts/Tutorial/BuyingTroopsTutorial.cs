using System;
using UnityEngine;

namespace AutoBattler
{
    internal class BuyingTroopsTutorial : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void OnEnable()
        {
            InitCamera();
        }

        private void InitCamera()
        {
            var cameraGameobject = GameObject.FindGameObjectWithTag("CanvasCamera");
            if (cameraGameobject.TryGetComponent(out Camera camera))
            {
                _canvas.worldCamera = camera;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
                Destroy(gameObject);
        }
    }
}
