using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [Serializable]
    public struct ExperienceRewardData
    {
        public int Level;
        public bool IsCollected;

        public ExperienceRewardData(int level, bool isCollected)
        {
            Level = level;
            IsCollected = isCollected;
        }

        internal ExperienceRewardData Collected()
        {
            return new ExperienceRewardData(this.Level, true);
        }
    }
}
