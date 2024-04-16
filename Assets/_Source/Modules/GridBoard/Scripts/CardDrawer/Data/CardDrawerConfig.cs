using PersistentData;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;

namespace GridBoard
{
    [CreateAssetMenu(menuName = "CardDrawer/Config")]
    internal class CardDrawerConfig : ScriptableObject
    {
        [SerializeField] private List<UnitPurchaseDataSO> _unitDatas;
        [SerializeField] private CardDrawerOdds _basicCardDrawerOdds;
        [SerializeField] private LevelVariable _mainLevel;

        internal string GetRandomUnitForDrawer(int currentLevel)
        {
            var commonUnitsOdds = _basicCardDrawerOdds.CommonOdds;
            var rareUnitsOdds = _basicCardDrawerOdds.RareOdds;

            var availableUnits = _unitDatas.Where(n => n.LevelRequirement <=  currentLevel).ToList();

            var commonUnits = availableUnits.Where(n => n.UnitView.Rarity == 0).ToList();
            var rareUnits = availableUnits.Where(n => n.UnitView.Rarity == 1).ToList();

            if (rareUnits.Count == 0)
            {
                return commonUnits[Random.Range(0, commonUnits.Count)].UnitView.Name.ToString();
            }
            else
            {
                float randomNumber = Random.Range(0f, 1f);
                if (randomNumber >= 1 - rareUnitsOdds) 
                {
                    return rareUnits[Random.Range(0, rareUnits.Count)].UnitView.Name.ToString();
                }
                else
                {
                    return commonUnits[Random.Range(0, commonUnits.Count)].UnitView.Name.ToString();
                }
            }
        }

        internal string GetRandomUnit(CardDrawerOdds cardDrawerOdds)
        {
            int currentLevel = _mainLevel.Value;
            var commonUnitsOdds = cardDrawerOdds.CommonOdds;
            var rareUnitsOdds = cardDrawerOdds.RareOdds;

            var availableUnits = _unitDatas.Where(n => n.LevelRequirement <= currentLevel).ToList();

            var commonUnits = availableUnits.Where(n => n.UnitView.Rarity == 0).ToList();
            var rareUnits = availableUnits.Where(n => n.UnitView.Rarity == 1).ToList();

            if (rareUnits.Count == 0)
            {
                return commonUnits[Random.Range(0, commonUnits.Count)].UnitView.Name.ToString();
            }
            else
            {
                float randomNumber = Random.Range(0f, 1f);
                if (randomNumber >= 1 - rareUnitsOdds)
                {
                    return rareUnits[Random.Range(0, rareUnits.Count)].UnitView.Name.ToString();
                }
                else
                {
                    return commonUnits[Random.Range(0, commonUnits.Count)].UnitView.Name.ToString();
                }
            }
        }

        internal List<UnitPurchaseDataSO> GetRandomUnitsForLuckyDialog()
        {
            var rareUnits = _unitDatas.Where(n => n.UnitView.Rarity == 1).ToList();
            List<UnitPurchaseDataSO> result = new List<UnitPurchaseDataSO>();
            for (int i = 0; i < 3; i++)
            {
                result.Add(rareUnits[Random.Range(0, rareUnits.Count)]);
            }
            return result;
        }
    }
}
