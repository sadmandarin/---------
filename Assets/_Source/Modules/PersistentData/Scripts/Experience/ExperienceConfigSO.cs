using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Experience/Config")]
    public class ExperienceConfigSO : ScriptableObject
    {
        [field: SerializeField] public List<ExperienceLevel> ExperienceLevelList { get; private set; }
        [field: SerializeField] internal List<UnitLevelExperience> UnitExperiences { get; private set; }
        [field: SerializeField] internal List<UnitLevelExperience> HeroExperiences { get; private set; }

        public int GetUserLevelByExperience(int experience)
        {
            ExperienceLevel nextLevelData;
            if (IsThereNextLevel(experience))
            {
               nextLevelData = ExperienceLevelList.First(n => n.ExperienceToNextLevel > experience);
            }
            else
            {
                nextLevelData = ExperienceLevelList[ExperienceLevelList.Count - 1];
            }
            var nextLevelIndex = ExperienceLevelList.IndexOf(nextLevelData);
            return nextLevelIndex - 1;
        }

        public float GetProgress(int currentExperience, ExperienceLevel nextLevel)
        {
            var experienceForNextLevel = nextLevel.ExperienceToNextLevel;
            int userLevel = GetUserLevelByExperience(currentExperience);
            ExperienceLevel currentLevelData = ExperienceLevelList[userLevel];
            int previousLevelIndex = Mathf.Clamp(ExperienceLevelList.IndexOf(nextLevel) - 1, 0, ExperienceLevelList.Count);
            ExperienceLevel previousLevelData = ExperienceLevelList[previousLevelIndex];
            float progress = Mathf.Clamp((float)(currentExperience - previousLevelData.ExperienceToNextLevel) / (float)(experienceForNextLevel - previousLevelData.ExperienceToNextLevel), 0, 1);
            return progress;
        }

        private bool IsThereNextLevel(int experience)
        {
            return ExperienceLevelList.Any(n => n.ExperienceToNextLevel > experience);
        }

        internal int GetExperienceForUnit(int rarity, int level)
        {
            return UnitExperiences.First(n => n.Rarity == rarity).Experience[level];
        }
        internal int GetExperienceForHero(int rarity, int level)
        {
            return HeroExperiences.First(n => n.Rarity == rarity).Experience[level];
        }
    }

    [Serializable]
    internal class UnitLevelExperience
    {
        public int Rarity;
        public int[] Experience;
    }
}
