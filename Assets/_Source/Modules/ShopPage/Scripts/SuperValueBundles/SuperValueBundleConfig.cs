using PersistentData;
using UnityEngine;

namespace ShopPage
{
    [CreateAssetMenu(menuName = "Shop/SuperValueBundleConfig")]
    internal class SuperValueBundleConfig : ScriptableObject
    {
        [SerializeField] private SoldierRecruitConfig _soldierRecruitConfig;
        [SerializeField] private FloatVariableSO _coins, _gems;

        internal void AddSoldierAndResouces(int numberOfTroops, int coins, int gems)
        {
            _soldierRecruitConfig.AddNewTroops(numberOfTroops, 1);
            _coins.Value += coins;
            _gems.Value += gems;
        }
    }
}
