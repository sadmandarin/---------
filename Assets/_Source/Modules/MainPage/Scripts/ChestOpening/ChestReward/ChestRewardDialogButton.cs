using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitsData;
using Lean.Localization;

namespace MainPage
{
    internal class ChestRewardDialogButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ChestRewardDialog _dialog;
        [SerializeField] private Camera _canvasCamera;
        [SerializeField] private MainPageObjectsHider _mainPageObjectsHider;
        [SerializeField] private string _name;
        [SerializeField] private UnitViewSO _unitViews;

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenDialog);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenDialog);
        }

        private void OpenDialog()
        {
            var dialog = Instantiate(_dialog);
            dialog.Init();
            _mainPageObjectsHider.ToggleVisibility();
        }
    }
}
