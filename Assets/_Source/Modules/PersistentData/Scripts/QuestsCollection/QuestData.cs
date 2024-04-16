using System;

namespace PersistentData
{
    [Serializable]
    public struct QuestData
    {
        public int IndexOfQuest;
        public int TimesCompleted;
        public bool ClaimedReward;

        public QuestData(int indexOfQuest, int timesCompleted, bool claimedRewardForThisQuest)
        {
            IndexOfQuest = indexOfQuest;
            TimesCompleted = timesCompleted;
            ClaimedReward = claimedRewardForThisQuest;
        }

        public QuestData CompleteQuest()
        {
            return new QuestData(this.IndexOfQuest, this.TimesCompleted + 1, this.ClaimedReward);
        }

        public QuestData ClaimReward()
        {
            return new QuestData(this.IndexOfQuest, this.TimesCompleted, true);
        }
    }
}
