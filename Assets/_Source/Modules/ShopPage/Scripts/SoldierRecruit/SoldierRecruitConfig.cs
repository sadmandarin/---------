using PersistentData;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;

namespace ShopPage
{
    [CreateAssetMenu(menuName = "Shop/SoldierRecruitConfig")]
    internal class SoldierRecruitConfig : ScriptableObject
    {
        [SerializeField] private UnitViewsListSO _unitViews;
        [SerializeField] private UnitAdder _unitAdder;

        internal List<UnitViewSO> AddNewTroops(int numberOfCards, int level)
        {
            var rareTroops = _unitViews.UnitViews.Where(n => n.Rarity == 1).ToList();
            List<UnitViewSO> cardsToGet = new List<UnitViewSO>();
            for (int i = 0; i < numberOfCards; i++)
            {
                var randomTroop = rareTroops[UnityEngine.Random.Range(0, rareTroops.Count)];
                cardsToGet.Add(randomTroop);
                _unitAdder.AddUnit(randomTroop.Name.ToString(), level);
            }

            return cardsToGet;
        }
    }
}
