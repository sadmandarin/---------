using Lean.Localization;
using PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace HeadHunt
{
    internal class HeadHuntAttackInformation : MonoBehaviour
    {
        [SerializeField] private Text _attackText;
        [SerializeField] private int _maxAttacks = 10;
        [SerializeField] private Text _timesRemainingText;
        [SerializeField] private LeanPhrase _timeRemainingPhrase;

        private LevelVariable _levelVariable;
        private IntVariableSO _timesRemainingVariable;
        private int _lastLevelPlayed = -1;

        internal void Init(LevelVariable levelVariable, IntVariableSO timesRemaining)
        {
            _levelVariable = levelVariable;
            _timesRemainingVariable = timesRemaining;
            UpdateAttacksText();
            UpdateTimesRemainingText();
            _lastLevelPlayed = _levelVariable.Value;

            //_levelVariable.OnValueChanged += HandleLevelChanged;
        }

        private void OnEnable()
        {
            if (_levelVariable == null || _timesRemainingVariable == null)
                return;
            HandleLevelChanged();
            UpdateAttacksText();
            UpdateTimesRemainingText();
        }

        private void UpdateTimesRemainingText()
        {
            var localizedString = LeanLocalization.GetTranslationText(_timeRemainingPhrase.name);
            _timesRemainingText.text = string.Format(localizedString, _timesRemainingVariable.Value.ToString());
        }

        private void UpdateAttacksText()
        {
            _attackText.text = _levelVariable.Value + "/" + _maxAttacks.ToString();
        }

        private void HandleLevelChanged(int newLevel = 0)
        {
            newLevel = _levelVariable.Value;
            if (newLevel == 1 && _lastLevelPlayed == _maxAttacks)
            {
                _timesRemainingVariable.Value -= 1;
                _lastLevelPlayed = newLevel;
            }
            else
            {
                _lastLevelPlayed = newLevel;
            }
        }
    }
}
