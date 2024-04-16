using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class TestGetOffset : MonoBehaviour
    {
        [SerializeField] private Transform _transform1;
        [SerializeField] private Transform _transform2;
        [SerializeField] private Vector3 _poisitionOffset, _rotationOffset;

        [ContextMenu(nameof(GetOffset))]
        public void GetOffset()
        {
            _poisitionOffset = _transform2.position - _transform1.position;
            _rotationOffset = _transform2.eulerAngles - _transform1.eulerAngles;
        }

        [ContextMenu(nameof(ApplyOffset))]
        public void ApplyOffset()
        {
            _transform1.position += _poisitionOffset;
            _transform1.eulerAngles += _rotationOffset;
        }
    }
}
