using System;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Variables/LevelVariable")]
    public class LevelVariable : ScriptableObject
    {
        [SerializeField] private int _maxLevel;
        [SerializeField] private bool _resetOnMaxValue;
        [SerializeField, Min(1)] private int _value = 1;

        public int Value
        {
            get { return _value; }
            set
            {
                if (value > _maxLevel)
                {
                    _value = _resetOnMaxValue ? 1 : _maxLevel;
                }
                else
                {
                    _value = value;
                }
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<int> OnValueChanged;
    }
}