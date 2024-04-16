using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class LuckyProgress : MonoBehaviour
    {
        [SerializeField] private LuckyConfigSO _config;
        [SerializeField] private Text _progressText;
        [SerializeField] private Image _progressBarImage;
        [SerializeField] private IntVariableSO _luckyBonus;
        [SerializeField] private Text _timesBoxes;
        [SerializeField] private GameObject _boxEffect;
        [SerializeField] private Sprite _notFullProgress, _progressFull;

        private void OnEnable()
        {
            _luckyBonus.OnValueChanged += UpdateProgress;
            UpdateProgress();
        }

        private void OnDisable()
        {
            _luckyBonus.OnValueChanged -= UpdateProgress;
        }

        private void UpdateProgress(int value = 0)
        {
            var progress = (float)_luckyBonus.Value / (float)_config.LuckyBonusRequirement;
            var progressClamped = Mathf.Clamp(progress, 0, 1);
            _progressBarImage.sprite = progressClamped == 1 ? _progressFull : _notFullProgress;
            //_progressBarImage.fillMethod = Image.FillMethod.Horizontal;
            _progressBarImage.fillAmount = progressClamped;
            _progressText.text = $"{_luckyBonus.Value}/{_config.LuckyBonusRequirement}";
            var boxesCount = _luckyBonus.Value / _config.LuckyBonusRequirement;
            if (_timesBoxes != null && _boxEffect != null)
            {
                _timesBoxes.text = "x" + boxesCount;
                _timesBoxes.gameObject.SetActive(boxesCount >= 1);
                _boxEffect.SetActive(boxesCount >= 1);
            }
        }
    }
}
