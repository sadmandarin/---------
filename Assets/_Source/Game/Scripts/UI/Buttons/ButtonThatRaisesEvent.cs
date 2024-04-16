using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    public class ButtonThatRaisesEvent : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private VoidEventChannelSO _event;

        private void OnEnable()
        {
            _button.onClick.AddListener(RaiseEvent);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RaiseEvent);
        }

        private void RaiseEvent()
        {
            _event.RaiseEvent();
        }
    }
}
