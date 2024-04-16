using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Heroes/HeroPrefabsList")]
    public class HeroPrefabsList : ScriptableObject
    {
        public GameObject GetPrefabByHeroName(string name) => _prefabs.First(n => n.HeroView.HeroName.ToString() == name).HeroPrefab;

        [field: SerializeField] private List<HeroPrefabData> _prefabs;
    }

}
