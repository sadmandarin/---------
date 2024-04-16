using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    [CreateAssetMenu(menuName = "CardDrawer/Progression")]
    internal class CardDrawerCostProgression : ScriptableObject
    {
        [SerializeField] private IntVariableSO _cardDrawerProgressionIndex;
        [SerializeField] private float _increment;
        [SerializeField] private float[] _predeterminedValues;
        [SerializeField] private float _maxValue = 120000;

        internal float GetCost()
        {
            if (_cardDrawerProgressionIndex.Value >= _predeterminedValues.Length)
            {
                var lastPredeterminedValue = _predeterminedValues[_predeterminedValues.Length - 1];
                var elementsAfterLastPredeterminedValue = (_cardDrawerProgressionIndex.Value - _predeterminedValues.Length + 1);
                return Mathf.Clamp(lastPredeterminedValue + (_increment * elementsAfterLastPredeterminedValue), 0, _maxValue);
            }
            else
            {
                return _predeterminedValues[_cardDrawerProgressionIndex.Value];
            }
        }

        internal void IncreaseIndex()
        {
            _cardDrawerProgressionIndex.Value++;
        }

        internal bool ReachedLimit()
        {
            if (GetCost() >= _maxValue)
                return true;
            else
                return false;
        }
    }
}
