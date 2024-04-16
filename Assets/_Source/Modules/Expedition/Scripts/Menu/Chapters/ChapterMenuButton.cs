using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ChapterMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _levelList, _chapterList, _backButton;

        private bool _showingChapters;

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleView);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleView);
        }

        internal void ToggleView()
        {
            SwitchChapterView(!_showingChapters);
        }

        private void SwitchChapterView(bool showChapters)
        {
            _showingChapters = showChapters;
            _chapterList.SetActive(showChapters);
            _levelList.SetActive(showChapters == false);
            _backButton.SetActive(showChapters == false);
        }
    }
}
