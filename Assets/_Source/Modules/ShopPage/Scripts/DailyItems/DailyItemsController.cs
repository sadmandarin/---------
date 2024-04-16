using Lean.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnitsData;
using UnityEngine;
using PersistentData;

namespace ShopPage
{
    internal class DailyItemsController : MonoBehaviour
    {
        [SerializeField] private DailyItemBase[] _dailyItems;
        [SerializeField] private DailyItemFree _freeDailyItem;
        [SerializeField] private List<UnitViewSO> _unitViews;
        [SerializeField] private int _gemsToAdd;
        [SerializeField] private DailyTroopsCollection _dailyTroopsCollection;

        private void Start()
        {
            UpdateFreeDaily();
            TryLoadDailyTroops(out bool loaded);
            if (loaded == false)
                UpdateDailyTroops();
        }

        internal void UpdateFreeDaily()
        {
            _freeDailyItem.Init(_gemsToAdd);
        }

        private void TryLoadDailyTroops(out bool loaded)
        {
            if (_dailyTroopsCollection.CollectionValue.Count == _dailyItems.Count())
            {
                SetUpDailyItemsByName(_dailyTroopsCollection.CollectionValue.Select(n => n.Name).ToList());
                loaded = true;
            }
            else
            {
                loaded = false;
            }
        }

        internal void UpdateDailyTroops()
        {
            var troopNames = new List<string>();
            for (int i = 0; i < _dailyItems.Length; i++)
            {
                UnitViewSO randomUnit = null;
                int price = 0;
                if (i == 0)
                {
                    var commonTroops = _unitViews.Where(n => n.Rarity == 0).ToList();
                    randomUnit = commonTroops[Random.Range(0, commonTroops.Count)];
                    price = 100;
                }
                if (i == 1)
                {
                    var rareTroops = _unitViews.Where(n => n.Rarity == 1).ToList();
                    randomUnit = rareTroops[Random.Range(0, rareTroops.Count)];
                    price = 200;
                }
                var troopName = LeanLocalization.GetTranslationText(randomUnit.Name.ToString());
                troopNames.Add(randomUnit.Name.ToString());
                _dailyItems[i].SetUp(randomUnit.Icon, troopName, randomUnit.Rarity, price, randomUnit.Name.ToString());
            }
            _dailyTroopsCollection.AddTroops(troopNames);
        }

        private void SetUpDailyItemsByName(List<string> names)
        {
            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];
                var unit = _unitViews.FirstOrDefault(n => n.Name.ToString() == name);
                var price = unit.Rarity == 1 ? 200 : 100;
                if (unit != null)
                {
                    var troopName = LeanLocalization.GetTranslationText(unit.Name.ToString());
                    _dailyItems[i].SetUp(unit.Icon, troopName, unit.Rarity, price, unit.Name.ToString());
                }
            }
        }
    }
}
