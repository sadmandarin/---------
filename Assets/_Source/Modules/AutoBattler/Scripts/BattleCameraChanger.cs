using DG.Tweening;
using UnityEngine;

namespace AutoBattler
{
    internal class BattleCameraChanger : MonoBehaviour
    {
        [SerializeField] private Vector3 _positionOffset, _rotationOffset;
        [SerializeField] private Transform _camera, _board;
        [SerializeField] private float _timeToMoveInSeconds = 1;

        private Vector3 _previousPosition, _previousRotation;
        private bool _hasCameraMoved;

        internal void ChangeCamera()
        {
            _previousPosition = _camera.position;
            _previousPosition = _camera.eulerAngles;

            _camera.DOMove(_board.position + _positionOffset, _timeToMoveInSeconds);
            _camera.DORotate(_board.eulerAngles + _rotationOffset, _timeToMoveInSeconds);

            _hasCameraMoved = true;
        }

        internal void ResetCamera()
        {
            if (_hasCameraMoved == false)
                return;

            _camera.DOKill();
            _camera.position = _previousPosition;
            _camera.eulerAngles = _previousRotation;
        }
    }
}
