using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class PaladinShield : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position + _offset) ;
        }
    }
}
