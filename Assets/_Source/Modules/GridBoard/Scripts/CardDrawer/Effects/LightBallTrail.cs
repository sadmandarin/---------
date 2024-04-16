using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    public class LightBallTrail : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _timeToMove;
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private Transform _target;
        [SerializeField] private Canvas _canvas;

        private Vector3 _targetPosition;

        internal void Init(Vector3 targetPosition, bool convertFromWorldSpaceToScreen)
        {
            var halfTheScreenSize = new Vector3(_canvasScaler.referenceResolution.x * 0.5f, _canvasScaler.referenceResolution.y * 0.5f, 0);
            if (convertFromWorldSpaceToScreen)
            {
                _targetPosition = Camera.main.WorldToScreenPoint(targetPosition) - halfTheScreenSize;
            }
            else
            {
                _targetPosition = _canvas.worldCamera.WorldToScreenPoint(targetPosition) - halfTheScreenSize;
            }
        }

        

        [ContextMenu(nameof(InitTest))]
        internal void InitTest()
        {
            Init(_target.position, false);
            Debug.Log(_targetPosition);
        }

        [ContextMenu(nameof(Move))]
        internal Tween Move()
        {
            _particleSystem.Play();
            return transform.DOLocalMove(_targetPosition, _timeToMove);
        }

        [ContextMenu(nameof(ResetPosition))]
        internal void ResetPosition()
        {
            transform.localPosition = Vector3.zero;
        }

    }
}
