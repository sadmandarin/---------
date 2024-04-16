using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroOriginItemButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private VoidEventChannelSO _event;
        [SerializeField] private GameObject _parent;
        [SerializeField] private HeroOriginDialog _dialog;

        private void OnEnable()
        {
            _button.onClick.AddListener(RaiseEventAndCloseDialog);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RaiseEventAndCloseDialog);
        }

        private void RaiseEventAndCloseDialog()
        {
            Destroy(_parent);
            _event.RaiseEvent();
            _dialog.RaiseOnMovingEvent();
        }
    }
}
