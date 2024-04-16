using System;
using UnityEngine;

namespace DragAndDrop
{
    public class  DragRoot : MonoBehaviour
    {
        public Action<GameObject> Dropped, StartedDragging;

        [SerializeField] private DragInput _input;
        [SerializeField] private LayerMask _layerMask;

        private Dragger _currentDragger;
        private bool _isActivated;

        public void Init()
        {
            _currentDragger = new Dragger(Camera.main);
        }

        public void Activate()
        {
            _isActivated = true;
            _input.TouchStarted += OnTouchStarted;
            _input.Touching += OnTouching;
            _input.TouchEnded += OnTouchEnded;
        }

        public void Deactivate()
        {
            _isActivated = false;
            _input.TouchStarted -= OnTouchStarted;
            _input.Touching -= OnTouching;
            _input.TouchEnded -= OnTouchEnded;
        }
        private void OnTouchStarted()
        {
            if (_isActivated == false)
                return;

            _currentDragger.StartDrag(_input.CursorPosition);
            if (_currentDragger.CanDrag)
                StartedDragging?.Invoke(_currentDragger.DraggedObject);
        }
        private void OnTouching()
        {
            if (_isActivated == false)
                return;

            if (_currentDragger.CanDrag)
                _currentDragger.Drag(_input.CursorPosition, _layerMask);
        }

        private void OnTouchEnded()
        {
            if (_isActivated == false)
                return;

            if (_currentDragger.CanDrag)
            {
                var objectThatWasDragged = _currentDragger.DraggedObject;
                _currentDragger.StopDrag();
                Dropped?.Invoke(objectThatWasDragged);
            }
        }
    }
}
