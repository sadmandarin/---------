using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HeroPage
{
    [RequireComponent(typeof(BoxCollider))]
    public class RotatingByMouse : MonoBehaviour
    {
        private Vector3 _mouseStartPosition;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private Transform _objectToRotate;

        private void OnMouseDrag()
        {
            float deltaX = Input.mousePosition.x - _mouseStartPosition.x;
            _objectToRotate.Rotate(Vector3.up, -deltaX * _rotationSpeed * Time.deltaTime);
            _mouseStartPosition = Input.mousePosition;
        }

        private void OnMouseDown()
        {
            _mouseStartPosition = Input.mousePosition;
        }
    }
}
