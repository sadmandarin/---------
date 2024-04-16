using UnityEngine;

namespace TerritoryPage
{
    [CreateAssetMenu(menuName = "Territory/TerritoryItem")]
    internal class TerritoryPageItemData : ScriptableObject
    {
        [field: SerializeField] public GameObject DialogPrefab;
        [field: SerializeField] public int LevelRequirement;
    }
}
