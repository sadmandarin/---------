using PersistentData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(menuName = "Quests/QuestReward")]
    internal class QuestRewardSO : ScriptableObject
    {
        [field:SerializeField] public List<QuestRewardData> ExtraRewards { get; private set; }
    }

    [Serializable]  
    internal struct QuestRewardData
    {
        public ExtraRewardBase ExtraReward;
        public int Quantity;
    }
}
