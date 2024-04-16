using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "ExtraReward/Chest")]
    public class ExtraRewardChest : ExtraRewardBase
    {
        [SerializeField] private ChestVariableSO _chest;

        public override void ClaimReward(int quantity = 0)
        {
            _chest.AddChest();
        }
    }
}
