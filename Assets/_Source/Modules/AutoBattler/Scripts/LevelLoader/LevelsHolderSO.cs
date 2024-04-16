using UnityEngine;

namespace AutoBattler
{
    [CreateAssetMenu(menuName = "Levels/LevelsHolder")]
    internal class LevelsHolderSO : ScriptableObject
    {
        [field:SerializeField] internal TextAsset[] AllLevels;
    }
}
