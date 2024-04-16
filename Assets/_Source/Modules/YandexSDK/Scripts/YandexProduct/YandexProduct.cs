using UnityEngine;

    public abstract class YandexProduct : ScriptableObject
    {
        [field: SerializeField] public string YandexId { get; private set; }
        public abstract void GetRewardForProduct();
    }
