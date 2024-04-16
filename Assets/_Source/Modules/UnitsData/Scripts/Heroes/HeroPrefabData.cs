using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Heroes/HeroPrefab")]
    internal class HeroPrefabData : ScriptableObject
    {
        [field: SerializeField] public HeroPresentationSO HeroView { get; private set; }
        [field: SerializeField] public GameObject HeroPrefab { get; private set; }
    }

}
