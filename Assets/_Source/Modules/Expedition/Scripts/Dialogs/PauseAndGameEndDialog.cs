using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class PauseAndGameEndDialog : MonoBehaviour
    {
        [SerializeField] private Text[] requirementTexts;

        [SerializeField] private ConquestLevelRequirementsConfig requirementsConfig;
        [SerializeField] private BackHomeButton _backHomeButton;

        internal void SetupData(int level)
        {
            _backHomeButton.SetUp(level);
            var levelRequirement = requirementsConfig.Requirements[level - 1];
            for (int i = 0; i < requirementTexts.Length; i++)
            {
                var requirement = levelRequirement.Requirements[i].Requirement;
                var requirementValues = levelRequirement.Requirements[i].RequirementValues;
                Debug.Log("Requirement values " + requirementValues.ToString());
                foreach (var requirementValue in requirementValues)
                {
                    Debug.Log("Requirement value: " + requirementValue);
                }
                
                string requirementText = requirement.GetLevelRequirementText(requirementValues);
                Debug.Log("Requirement text " + requirementText);
                requirementTexts[i].text = requirementText;
            }
        }
    }
}
