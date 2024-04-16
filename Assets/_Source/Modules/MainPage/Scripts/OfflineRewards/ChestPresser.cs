using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestPresser : MonoBehaviour
    {
        [SerializeField] private Button _button;

        internal Action ChestPressed;

        private void OnEnable()
        {
            _button.onClick.AddListener(RaiseChestPressed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RaiseChestPressed);
        }

        internal void RaiseChestPressed()
        {
            ChestPressed?.Invoke();    
        }

        
    }
}
