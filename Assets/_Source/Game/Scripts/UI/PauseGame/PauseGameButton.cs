using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class PauseGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private PauseDialog _pauseDialog;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private MenuSwitcher _menuSwitcher;
        [SerializeField] private MusicController _musicController;

        private void OnEnable()
        {
            _button.onClick.AddListener(SpawnDialog);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SpawnDialog);
        }

        private void SpawnDialog()
        {
            var dialog = Instantiate(_pauseDialog);
            dialog.InitCamera(_canvas.worldCamera);
            dialog.OnHomePressed += HandleOnHomePressed;
        }

        private void HandleOnHomePressed()
        {
            _menuSwitcher.SwitchToMainMenu();
            _musicController.PlayLosingSound();
        }
    }
}
