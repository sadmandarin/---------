using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Experience/ExpAdder")]
    public class ExperienceAdder : ScriptableObject
    {
        [SerializeField] private IntVariableSO _experience;
        [SerializeField] private ExperienceConfigSO _config;
        [SerializeField] private CollectedUnitsCollection _unitsCollection;
        [SerializeField] private IntEventChannelSO _onExperienceAdded;

        public void AddExperienceForBuyingTroops(int rarity, int level)
        {
            int experienceToGet = _config.GetExperienceForUnit(rarity, level);
            _onExperienceAdded.RaiseEvent(experienceToGet);
            _experience.Value += experienceToGet;
        }

        public void AddExperienceForUpgradingTroops(string name, int rarity, int level)
        {
            //Не используется больше
            if (_unitsCollection.TryAddUnit(name, level))
            {
                int experienceToGet = _config.GetExperienceForUnit(rarity, level);
                _experience.Value += experienceToGet;
                _onExperienceAdded.RaiseEvent(experienceToGet);
            }
        }

        public void AddExperienceForUnlockingNewHero(string heroName, int rarity, int level)
        {
            if (_unitsCollection.TryAddUnit(heroName, level))
            {
                int experienceToGet = _config.GetExperienceForHero(rarity, level);
                _experience.Value += experienceToGet;
                _onExperienceAdded.RaiseEvent(experienceToGet);
            }
        }
    }
}
