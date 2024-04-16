using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "HeroCollection/Collection")]
    public class HeroCollection : PersistentCollection<HeroData>
    {
        public Action<string, int> HeroUnlocked;

        public HeroData GetSelectedHero() => CollectionValue.FirstOrDefault(n => n.Selected == true);
        public bool IsHeroSelected => CollectionValue.Any(n => n.Selected);
        public bool HasHeroInCollection(string heroName) => CollectionValue.Any(n=> n.Name == heroName && n.Unlocked) == false;

        [SerializeField] private ExperienceAdder _experienceAdder;
        [SerializeField] private List<HeroPresentationSO> _heroRarities;

        public void SetData(List<string> heroNames)
        {
            foreach (string heroName in heroNames)
            {
                CollectionValue.Add(new HeroData(heroName));
            }
            CollectionChanged?.Invoke();
        }

        public void SelectHero(string heroName)
        {
            if (IsHeroSelected)
            {
                HeroData alreadySelectedHero = GetSelectedHero();
                int indexOfSelectedHero = CollectionValue.IndexOf(alreadySelectedHero);
                CollectionValue[indexOfSelectedHero] = alreadySelectedHero.Unselect();
                if (alreadySelectedHero.Name == heroName)
                {
                    return;
                }
            }
            HeroData heroThatWillBeSelected = CollectionValue.FirstOrDefault(n => n.Name == heroName);
            int indexOfHeroThatWillBeSelected = CollectionValue.IndexOf(heroThatWillBeSelected);
            CollectionValue[indexOfHeroThatWillBeSelected] = heroThatWillBeSelected.Select();
            CollectionChanged?.Invoke();
        }

        public void AddHero(string heroName)
        {
            HeroData heroThatWillBeAdded = CollectionValue.FirstOrDefault(n => n.Name == heroName);
            int indexOfHeroThatWillBeAdded = CollectionValue.IndexOf(heroThatWillBeAdded);
            if (heroThatWillBeAdded.Unlocked)
            {
                CollectionValue[indexOfHeroThatWillBeAdded] = heroThatWillBeAdded.AddShard();
            }
            else
            {
                bool firstHeroUnlocked = CollectionValue.Any(n => n.Unlocked) == false;
                CollectionValue[indexOfHeroThatWillBeAdded] = heroThatWillBeAdded.Unlock();
                if (firstHeroUnlocked)
                    CollectionValue[indexOfHeroThatWillBeAdded] = CollectionValue[indexOfHeroThatWillBeAdded].Select();
                var heroRarity = _heroRarities.First(n => n.HeroName.ToString() == heroName).Rarity;
                _experienceAdder.AddExperienceForUnlockingNewHero(heroName, heroRarity, 1);
                HeroUnlocked?.Invoke(heroName, 1);
            }
            CollectionChanged?.Invoke();
        }

        public void UpgradeHero(string heroName, int shardsForUpgrade)
        {
            HeroData heroToUpgrade = CollectionValue.First(n => n.Name == heroName);
            int indexOfHeroToUpgrade = CollectionValue.IndexOf(heroToUpgrade);
            CollectionValue[indexOfHeroToUpgrade] = CollectionValue[indexOfHeroToUpgrade].AddLevel();
            CollectionValue[indexOfHeroToUpgrade] = CollectionValue[indexOfHeroToUpgrade].RemoveShards(shardsForUpgrade);

            HeroUnlocked?.Invoke(CollectionValue[indexOfHeroToUpgrade].Name, CollectionValue[indexOfHeroToUpgrade].Level);
            var heroRarity = _heroRarities.First(n => n.HeroName.ToString() == heroName).Rarity;
            _experienceAdder.AddExperienceForUnlockingNewHero(heroName, heroRarity, CollectionValue[indexOfHeroToUpgrade].Level);
            CollectionChanged?.Invoke();
        }

        public List<HeroData> SortedHeroes()
        {
            var unlockedHeroes = CollectionValue.Where(n => n.Unlocked).ToList();
            var lockedHeroes = CollectionValue.Where(n => n.Unlocked == false).ToList();
            return unlockedHeroes.Concat(lockedHeroes).ToList();
        }
    }
}
