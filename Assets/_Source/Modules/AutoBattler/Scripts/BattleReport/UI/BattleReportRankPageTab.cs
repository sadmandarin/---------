using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class BattleReportRankPageTab : MonoBehaviour
    {
        internal BattleReportStat Stat => _stat;

        public string TranslationName { get => _translationName; set => _translationName = value; }

        internal Action<BattleReportStat> TabPressed;

        [SerializeField] private BattleReportStat _stat;
        [SerializeField] private Button _button;
        [SerializeField] private Image _selectedImage;
        [SerializeField] private Image _unselectedImage;
        [SerializeField] private string _translationName;

        private void OnEnable()
        {
            _button.onClick.AddListener(Click);

        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
        }

        internal void Select()
        {
            _selectedImage.gameObject.SetActive(true);
            _unselectedImage.gameObject.SetActive(false);
        }

        internal void Unselect()
        {
            _selectedImage.gameObject.SetActive(false);
            _unselectedImage.gameObject.SetActive(true);
        }

        private void Click()
        {
            TabPressed?.Invoke(_stat);
        }
    }
}
