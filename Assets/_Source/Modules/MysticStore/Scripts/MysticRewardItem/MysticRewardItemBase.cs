using UnityEngine;
#if UNITY_EDITOR
using MysticStoreEditor;
#endif

namespace MysticStore
{
    public abstract class MysticRewardItemBase : ScriptableObject
    {
        [field:SerializeField
#if UNITY_EDITOR
            , ScriptableObjectId 
            #endif
            ]
        public string GUID { get; private set; }
        public abstract Sprite Icon();
        public abstract int Rarity();
        public abstract int Price();
        public abstract int TroopStars();
        public abstract bool IsHero();
        public abstract void ClaimReward();
        public abstract string Description();
        
    }
}
