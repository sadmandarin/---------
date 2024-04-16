using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitsData;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class HeroesChestTip : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private HeroPresentationsList _heroesList;
        [SerializeField] private LeanPhrase _rarePhrase;
        [SerializeField] private LeanPhrase _epicPhrase;

        private void OnEnable()
        {
            _text.text = GetText();
        }

        private string GetText()
        {
            var rareHeroes = _heroesList.Heroes.Where(n => n.Rarity == 1).ToList();
            var epicHeroes = _heroesList.Heroes.Where(n => n.Rarity == 2).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<color=blue>" + LeanLocalization.GetTranslationText(_rarePhrase.name) +"</color>" + " 40%");
            sb.Append("<b>");
            for (int i = 0; i < rareHeroes.Count; i++)
            {
                if (i > 0)
                    sb.Append("/");
                HeroPresentationSO hero = rareHeroes[i];
                sb.Append(LeanLocalization.GetTranslationText(hero.HeroName.ToString()));
                if (i % 2 == 0 && i != rareHeroes.Count - 1 && i != 0)
                    sb.AppendLine();
                if (i == rareHeroes.Count - 1)
                    sb.Append("</b>");
            }
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("<color=purple>" + LeanLocalization.GetTranslationText(_rarePhrase.name) + "</color>" + " 60%");
            sb.Append("<b>");
            for (int i = 0; i < epicHeroes.Count; i++)
            {
                if (i > 0)
                    sb.Append("/");
                HeroPresentationSO hero = epicHeroes[i];
                sb.Append(LeanLocalization.GetTranslationText(hero.HeroName.ToString()));
                if ((i + 1) % 3 == 0 && i != epicHeroes.Count - 1)
                    sb.AppendLine();
                if (i == epicHeroes.Count - 1)
                    sb.Append("</b>");
            }
            return sb.ToString();
        }
    }
}
