using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroSkillItemUpgradeInfo : MonoBehaviour
    {
        [SerializeField] private Text _text;

        internal void Set(HeroSkillDescription skillDescription, int currentLevel)
        {
            var localizedName = LeanLocalization.GetTranslationText(skillDescription.LeanPhraseName.name);
            var statsString = "";
            for (int i = 0; i < skillDescription.Values.Length; i++)
            {
                if (i == currentLevel || skillDescription.Values.Length == 1)
                {
                    statsString += CustomTextFormatter.ToBoldAndBlack(skillDescription.Values[i].ToString() + skillDescription.ValueSuffix);
                }
                else
                {
                    statsString += CustomTextFormatter.ToGrey(skillDescription.Values[i].ToString() + skillDescription.ValueSuffix);
                }
                if (i !=  skillDescription.Values.Length - 1)
                    statsString += CustomTextFormatter.ToGrey(" / ");
            }
            _text.text = localizedName + "\n" + statsString;
        }
    }
}
