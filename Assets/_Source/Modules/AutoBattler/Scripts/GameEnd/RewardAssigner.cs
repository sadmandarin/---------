using PersistentData;
using UnityEngine;

namespace AutoBattler
{
    internal class RewardAssigner : MonoBehaviour
    {
        [SerializeField] private FloatVariableSO _money;

        internal void AssignRewards(int moneyWon)
        {
            _money.Value += moneyWon;
        }
    }
}
