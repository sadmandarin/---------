using System;
using UnityEngine;

namespace MainPage
{
    internal class ChestController : MonoBehaviour
    {
        [SerializeField] private ChestButton _chestButton;
        [SerializeField] private ChestDialog _chestDialogPrefab;
        [SerializeField] private Camera _canvasCamera;
        [SerializeField] private MainPageObjectsHider _mainPageHider;

        private void Awake()
        {
            _chestButton.ChestButtonPressed += HandleChestButtonPressed;
        }

        private void HandleChestButtonPressed()
        {
            var dialog = Instantiate(_chestDialogPrefab);
            dialog.Init(_canvasCamera);
            dialog.ChestDialogClosed += HandleDialogClosed;
            _mainPageHider.ToggleVisibility();
        }

        private void HandleDialogClosed()
        {
            _mainPageHider.ToggleVisibility();
        }
    }
}
