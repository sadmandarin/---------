using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroOriginButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private HeroOriginDialogSpawner _dialogSpawner;
        

        private void OnEnable()
        {
            _button.onClick.AddListener(ShowDialog);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ShowDialog);
        }

        private void ShowDialog()
        {
            _dialogSpawner.ShowDialog();
        }
    }
}
