using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroStatsDescriptionBubble : MonoBehaviour, ITipBubble
    {
        public bool IsActive { get; private set; }
        public Action<ITipBubble> TipShown { get; set; }    

        [SerializeField] private Text _text;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private LeanPhrase _health, _defense, _attack;

        
        internal void SetUp(float[] healths, float[] defenses = null, float[] attacks = null, int currentLevel = 1)
        {
            var healthString = "";
            var defenseString = "";
            var attackString = "";

            for (int i = 0; i < healths.Length; i++)
            {
                if (i == currentLevel)
                {
                    healthString += CustomTextFormatter.ToBoldAndBlack(healths[i].ToString());
                    defenseString += CustomTextFormatter.ToBoldAndBlack(defenses[i].ToString());
                    attackString += CustomTextFormatter.ToBoldAndBlack(attacks[i].ToString());
                }
                else
                {
                    healthString += healths[i].ToString();
                    defenseString += defenses[i].ToString();
                    attackString += attacks[i].ToString();
                }
                healthString += "/";
                defenseString += "/";
                attackString += "/";
            }

            _text.text = CustomTextFormatter.ToBoldAndBlack(LeanLocalization.GetTranslationText(_health.name) + ":") + ": \n" + healthString + "\n" + "\n"
                       + CustomTextFormatter.ToBoldAndBlack(LeanLocalization.GetTranslationText(_defense.name) + ":") + "\n" + defenseString + "\n" + "\n"
                       + CustomTextFormatter.ToBoldAndBlack(LeanLocalization.GetTranslationText(_attack.name) + ":") + "\n" + attackString + "\n" + "\n";
        }

        internal void ToggleVisibility()
        {
            if (IsActive)
                Hide();
            else
                Show();
        }

        public void Hide()
        {
            IsActive = false;
            _canvasGroup.alpha = 0;
        }

        public void Show()
        {
            IsActive = true;
            _canvasGroup.alpha = 1;
            TipShown?.Invoke(this);
        }
    }
}
