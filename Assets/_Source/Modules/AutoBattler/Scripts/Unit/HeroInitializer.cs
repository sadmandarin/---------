using PersistentData;
using UnityEngine;
using UnitsData;

namespace AutoBattler
{
    internal class HeroInitializer : MonoBehaviour
    {
        internal AutoBattlerUnit Hero => _hero;

        [SerializeField] private HeroCollection _heroCollection;
        [SerializeField] private HeroPrefabsList _heroPrefabs;
        
        private AutoBattlerUnit _hero;

        internal void InitializeHero(Transform heroPosition)
        {
            if (_heroCollection.IsHeroSelected == false)
                return;

            var selectedHero = _heroCollection.GetSelectedHero();
            var heroPrefab = _heroPrefabs.GetPrefabByHeroName(selectedHero.Name);
            var spawnedHero = Instantiate(heroPrefab, heroPosition);
            _hero = spawnedHero.GetComponent<AutoBattlerUnit>();
            _hero.SetLevel(selectedHero.Level);
        }
    }
}
