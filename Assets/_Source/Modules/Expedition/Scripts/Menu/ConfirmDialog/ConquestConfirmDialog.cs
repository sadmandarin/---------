using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestConfirmDialog : MonoBehaviour
    {
        internal Action<int> OnConfirmedPressed;

        [SerializeField] private Button _closeButton, _confirmButton;
        [SerializeField] private Text[] _requirementTexts;
        [SerializeField] private Text _titleText;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private ConquestLevelRequirementsConfig _requirementsConfig;
        [SerializeField] private LeanPhrase _missionTitlePhrase;

        private int _level;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(HandleOnClose);
            _confirmButton.onClick.AddListener(HandleOnConfirm);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(HandleOnClose);
            _confirmButton.onClick.RemoveListener(HandleOnConfirm);
        }

        private void HandleOnConfirm()
        {
            OnConfirmedPressed?.Invoke(_level);
        }

        private void HandleOnClose()
        {
            Destroy(gameObject);
        }

        internal void InitDialog(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }

        internal void SetupData(int level)
        {
            _level = level;
            var levelRequirement = _requirementsConfig.Requirements[level - 1];
            for (int i = 0; i < _requirementTexts.Length; i++)
            {
                var requirement = levelRequirement.Requirements[i].Requirement;
                var requirementValues = levelRequirement.Requirements[i].RequirementValues;
                string requirementText = requirement.GetLevelRequirementText(requirementValues);
                _requirementTexts[i].text = requirementText;
            }
            var localizedString = LeanLocalization.GetTranslationText(_missionTitlePhrase.name);



            _titleText.text = string.Format(localizedString, LevelHelper.NormalizeLevelForDisplay(level));
        }

        
    }
}
