using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public class CloseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(DestroyObject);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(DestroyObject);
        }

        private void DestroyObject()
        {
            Destroy(_objectToDestroy);
        }
    }
}
