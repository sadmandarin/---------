using PersistentData;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(menuName = "Quests/DailyProgressData")]
    internal class DailyProgressItemSO : ScriptableObject
    {
        [field: SerializeField] internal int ProgressPointsToUnlock { get;private set; }
        [field: SerializeField] internal Sprite LockedSprite { get;private set; }
        [field: SerializeField] internal Sprite UnlockedSprite { get;private set; }
        [field: SerializeField] internal ExtraRewardBase Reward{ get;private set; }
        [field: SerializeField] internal int QuantityOfReward{ get;private set; }
    }
}
