using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class BattleDifficultyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BattleDifficultyController _controller;
        [SerializeField] private BattleDifficulty _difficulty;
        [SerializeField] private int _levelWhenUnlocked;
        [SerializeField] private LevelVariable _mainLevel;
        [SerializeField] private BattleDifficultyConfig _config;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleOnClick);

            UpdateView();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnClick);
        }

        private void HandleOnClick()
        {
            if (_mainLevel.Value >= _levelWhenUnlocked)
            {
                _controller.ChangeDifficulty(_difficulty);
            }
            else
            {
                _controller.ShowTip(_levelWhenUnlocked);
            }
            
        }

        private void UpdateView()
        {
            bool isUnlocked = _mainLevel.Value >= _levelWhenUnlocked;
            var bgSprite = _config.GetDifficultyData(_difficulty).BgImage;

            _button.image.sprite = isUnlocked ? bgSprite : _config.InactiveButtonSprite;
        }
    }
}
