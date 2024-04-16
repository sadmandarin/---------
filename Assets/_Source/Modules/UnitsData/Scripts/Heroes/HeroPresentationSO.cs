using System.Collections;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Heroes/HeroPresentation")]
    public class HeroPresentationSO : ScriptableObject
    {
        [field: SerializeField] public LocalizedHeroName HeroName { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Sprite BoxIcon { get; private set; }
        [field: SerializeField] public int Rarity { get; private set; }
        [field: SerializeField] public GameObject PrefabForPresentionInUi { get; private set; }
    }

    public enum LocalizedHeroName
    {
        Alchemist,
        FrostEarl,
        TreeOfLife,
        Diana,
        Archon,
        Minstrel,
        Planck,
        ElementalEnvoy,
        BlackBeard
    }
}
