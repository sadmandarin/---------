using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class GridInputManager : MonoBehaviour
    {
        public Vector3 MousePosition => new Vector3(Input.mousePosition.x, Input.mousePosition.y, _sceneCamera.nearClipPlane);

        [SerializeField] private Camera _sceneCamera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _lengthOfRaycast = 100f;

        private Vector3 _lastPosition;

        internal Vector3 GetSelectedMapPosition()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = _sceneCamera.nearClipPlane;
            Ray ray = _sceneCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _lengthOfRaycast, _layerMask))
            {
                _lastPosition = hit.point;
            }
            return _lastPosition;
        }



        internal Ray GetRayUnderMouse()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = _sceneCamera.nearClipPlane;
            return _sceneCamera.ScreenPointToRay(mousePosition);
        }
    }
}
