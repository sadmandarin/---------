using System;
using System.Linq;
using UnitsData;
using UnityEngine;

namespace GridBoard
{
    [CreateAssetMenu]
    internal class TroopsViewsSO : ScriptableObject
    {
        [field: SerializeField] public UnitViewSO[] _troopViews;

        internal TroopViewData GetTroopViewByName(string name)
        {
            var troopView = _troopViews.FirstOrDefault(n => n.Name.ToString() == name);
            return new TroopViewData(troopView.Name.ToString(), troopView.Rarity, troopView.Icon);
        }
    }

    
    internal struct TroopViewData
    {
        public string Name;
        public int rarity;
        public Sprite Icon;

        public TroopViewData(string name, int rarity, Sprite icon)
        {
            Name = name;
            this.rarity = rarity;
            Icon = icon;
        }
    }
}
