using PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class ProgressPointsText : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _fillBar;
        [SerializeField] private IntVariableSO _progressPoints;

        private void OnEnable()
        {
            _progressPoints.OnValueChanged += UpdateText;
            UpdateText();
        }

        private void OnDisable()
        {
            _progressPoints.OnValueChanged -= UpdateText;
        }

        private void UpdateText(int points = 0)
        {
            _text.text = _progressPoints.Value.ToString();
            _fillBar.fillAmount = Mathf.Clamp((float)_progressPoints.Value / 100f, 0, 1f);
        }
    }
}
