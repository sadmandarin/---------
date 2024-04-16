using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Units/ViewsList")]
    public class UnitViewsListSO : ScriptableObject
    {
        [field: SerializeField] public List<UnitViewSO> UnitViews { get; private set; }

        public int GetRarityOfTroop(string troopName)
        {
            return UnitViews.First(n => n.Name.ToString() == troopName).Rarity;
        }
    }
}
