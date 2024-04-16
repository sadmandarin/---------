using UnityEngine;

namespace DragAndDrop
{
    internal class Dragger
    {
        internal GameObject DraggedObject => _currentDraggable == null ? null : _currentDraggable.gameObject;

        private Draggable _currentDraggable;
        private Camera _camera;

        internal Dragger(Camera camera)
        {
            _camera = camera;
        }

        internal bool CanDrag => _currentDraggable != null;

        internal void StartDrag(Vector3 cursorPosition)
        {
            Ray ray = _camera.ScreenPointToRay(cursorPosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.TryGetComponent(out Draggable draggable))
                {
                    _currentDraggable = draggable;
                    _currentDraggable.StartDrag();
                }
            }
        }

        internal void Drag(Vector3 cursorPosition, LayerMask layerMask)
        {
            Vector3 newPosition = Vector3.zero;
            Ray ray = _camera.ScreenPointToRay(cursorPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
            {
                newPosition = hit.point;
            }
            //Vector3 newDragPosition = _camera.ScreenToWorldPoint(cursorPosition);
            _currentDraggable.Drag(newPosition);
        }

        internal void StopDrag()
        {
            _currentDraggable.StopDragging();
            _currentDraggable = null;
        }
    }
}
