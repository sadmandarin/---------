using Lean.Localization;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;

namespace MainPage
{
    internal class HeroChestRewardLoot : MonoBehaviour
    {
        [SerializeField] private List<HeroPresentationSO> _heroesForDialog;
        [SerializeField] private float _chanceForEpicHero = 0.4f;
        [SerializeField] private float _chanceForRareHero = 0.6f;

        internal void GetRandomHero(out Sprite icon, out string name, out int rarity, out string defaultName)
        {
            float randomNumber = Random.Range(0f, 1f);
            HeroPresentationSO randomHero;
            if (randomNumber >= 1 -  _chanceForEpicHero)
            {
                var epicHeroes = _heroesForDialog.Where(n => n.Rarity == 2).ToList();
                randomHero = epicHeroes[UnityEngine.Random.Range(0, epicHeroes.Count)];
            }
            else
            {
                var rareHeroes = _heroesForDialog.Where(n => n.Rarity == 1).ToList();
                randomHero = rareHeroes[UnityEngine.Random.Range(0, rareHeroes.Count)];
            }
            icon = randomHero.Icon;
            name = LeanLocalization.GetTranslationText(randomHero.HeroName.ToString());
            rarity = randomHero.Rarity;
            defaultName = randomHero.HeroName.ToString();
        }
    }
}
