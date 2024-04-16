using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{
    internal class Draggable : MonoBehaviour
    {
        private Vector3 _startPosition;

        internal void StartDrag()
        {
            _startPosition = transform.position;
        }

        internal void Drag(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        internal void StopDragging()
        {
            transform.position = _startPosition;
        }
    }
}
