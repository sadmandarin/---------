using Lean.Localization;
using PersistentData;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;

namespace MainPage
{
    [CreateAssetMenu(menuName = "TroopChestRewardsConfig")]
    internal class TroopChestRewardsConfig : ScriptableObject
    {
        [SerializeField] private UnitPurchaseDataList _purchaseList;
        [SerializeField] private float _commonProbability = 0.4f;
        [SerializeField] private float _rareProbability = 0.6f;
        [SerializeField] private LevelVariable _currentLevel;
        [SerializeField] private UnitAdder _unitAdder;

        internal void GetRandomTroopData(out string defaultName, out Sprite troopIcon, out string localizedName, out int rarity)
        {
            var availableUnits = GetAllAvailableUnits();

            List<UnitPurchaseDataSO> commonTroops = availableUnits.Where(n => n.UnitView.Rarity == 0).ToList();
            var notCommonTroops = availableUnits.Where(n => n.UnitView.Rarity > 0).ToList();
            float randomNumber = UnityEngine.Random.Range(0f, 1f);
            UnitPurchaseDataSO randomTroop;
            
            if (randomNumber > 1 - _commonProbability || notCommonTroops.Count == 0)
            {
                randomTroop = commonTroops[UnityEngine.Random.Range(0, commonTroops.Count)];
            }
            else
            {
                randomTroop = notCommonTroops[UnityEngine.Random.Range(0, notCommonTroops.Count)];
            }
            defaultName = randomTroop.UnitView.Name.ToString();
            troopIcon = randomTroop.UnitView.Icon;
            localizedName = LeanLocalization.GetTranslationText(randomTroop.UnitView.Name.ToString());
            rarity = randomTroop.UnitView.Rarity;

        }

        internal List<UnitPurchaseDataSO> GetAllAvailableUnits()
        {
            var availableUnits = _purchaseList.UnitPurchaseData.Where(n => n.LevelRequirement < _currentLevel.Value).ToList();
            List<string> unlockedUnits = _unitAdder.GetAllUnlockedUnits();
            foreach (var unit in unlockedUnits)
            {
                UnitPurchaseDataSO unitData = _purchaseList.UnitPurchaseData.FirstOrDefault(n => n.UnitView.Name.ToString() == unit);
                if (unitData == null)
                    continue;
                if (availableUnits.Contains(unitData) == false)
                {
                    availableUnits.Add(unitData);
                }
            }

            return availableUnits;
        }
    }
}
