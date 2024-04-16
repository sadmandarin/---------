using System;

namespace PersistentData
{
    [Serializable]
    public struct ConquestLevelsData
    {
        public int Level;
        public int StarsFilled;
        public bool Unlocked;
        public bool Finished;
        public bool ClaimedReward;

        public ConquestLevelsData(int level, int starsFilled, bool unlocked, bool finished, bool claimedReward)
        {
            Level = level;
            StarsFilled = starsFilled;
            Unlocked = unlocked;
            Finished = finished;
            ClaimedReward = claimedReward;
        }

        public ConquestLevelsData FinsishLevel(int stars)
        {
            return new ConquestLevelsData(this.Level, stars, true, true, this.ClaimedReward);
        }

        public ConquestLevelsData Unlock()
        {
            return new ConquestLevelsData(this.Level, this.StarsFilled, true, false, this.ClaimedReward);
        }

        public ConquestLevelsData ClaimReward()
        {
            return new ConquestLevelsData(this.Level, this.StarsFilled, this.Unlocked, this.Finished, true);
        }
    }
}
