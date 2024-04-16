using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestLevelListFlag : MonoBehaviour
    {
        internal Action<int> OnFlagClicked;
        internal int Level => _level;

        public bool Unlocked { get => _unlocked; private set => _unlocked = value; }
        public bool Finished { get => _finished; private set => _finished = value; }

        [SerializeField] private int _level;
        [SerializeField] private Text _levelText;
        [SerializeField] private Image[] _stars;
        [SerializeField] private Image _flagImage;
        [SerializeField] private Sprite _filledStar;
        [SerializeField] private Sprite _unfilledStar;
        [SerializeField] private Sprite _lockedFlagSprite;
        [SerializeField] private Sprite _unlockedFlagSprite;
        [SerializeField] private Button _button;

        private bool _unlocked, _finished;

        internal void SetUp(int level, int starsFilled, bool unlocked, bool finished)
        {
            _unlocked = unlocked;
            _finished = finished;

            for (int i = 1; i <= starsFilled; i++)
            {
                _stars[i-1].sprite = _filledStar;
            }
            for (int i = starsFilled + 1; i < _stars.Length + 1; i++)
            {
                _stars[i-1].sprite = _unfilledStar;
            }

            _flagImage.sprite = unlocked ? _unlockedFlagSprite : _lockedFlagSprite;
            _button.enabled = unlocked;
            _levelText.text = LevelHelper.NormalizeLevelForDisplay(level).ToString();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ClickFlag);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ClickFlag);
        }

        private void ClickFlag()
        {
            OnFlagClicked?.Invoke(_level);
        }
    }
}
