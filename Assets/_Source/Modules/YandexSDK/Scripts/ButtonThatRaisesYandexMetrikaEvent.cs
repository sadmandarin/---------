using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YandexSDK
{
    public class ButtonThatRaisesYandexMetrikaEvent : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _eventName;

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
            YandexMetrika.Event(_eventName);
        }
    }
}
