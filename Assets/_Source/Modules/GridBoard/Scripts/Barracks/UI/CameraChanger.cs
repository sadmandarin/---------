using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class CameraChanger : MonoBehaviour
    {
        [SerializeField] private Transform _newPosition;
        [SerializeField] private float _timeToMove = 1f;

        internal void ChangeCameraTransform()
        {
            var cameraTransform = Camera.main.transform;
            DOTween.Sequence().Append(cameraTransform.DOMove(_newPosition.position, _timeToMove))
                              .Join(cameraTransform.DORotate(_newPosition.eulerAngles, _timeToMove));
        }
    }
}
