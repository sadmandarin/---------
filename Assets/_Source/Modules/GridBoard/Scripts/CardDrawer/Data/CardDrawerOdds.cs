using UnityEngine;
using UnityEngine.Serialization;

namespace GridBoard
{
    [CreateAssetMenu(menuName = "CardDrawer/Odds")]
    internal class CardDrawerOdds : ScriptableObject
    {
        [field:SerializeField, Range(0f, 1f)] internal float RareOdds { get; private set; }
        [field:SerializeField, Range(0f, 1f)] internal float CommonOdds { get; private set; }
        [field:SerializeField] internal Sprite CardBackIcon { get; private set; }
        [field:SerializeField, Min(1)] internal int LevelOfUnit{ get; private set; }
        [field:SerializeField, Min(1)] internal int Price { get; private set; }
    }
}
