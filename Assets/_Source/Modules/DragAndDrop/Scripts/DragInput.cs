using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    internal class DragInput : MonoBehaviour
    {
        internal Action TouchStarted, Touching, TouchEnded;

        internal Vector3 CursorPosition
        {
            get
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10;
                return mousePosition;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchStarted?.Invoke();
                return;
            }

            if (Input.GetMouseButton(0))
            {
                Touching?.Invoke();
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                TouchEnded?.Invoke();
            }
        }
    }
}
