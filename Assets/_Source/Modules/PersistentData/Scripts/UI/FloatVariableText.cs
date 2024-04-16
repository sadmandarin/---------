using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PersistentData
{
    public class FloatVariableText : MonoBehaviour
    {
        [SerializeField] private Text _text;

        [SerializeField] private FloatVariableSO _moneyVariable;

        private void OnEnable()
        {
            _moneyVariable.OnValueChanged += SetValue;
            SetValue(_moneyVariable.Value);
        }

        private void OnDisable()
        {
            _moneyVariable.OnValueChanged -= SetValue;
        }

        public void SetValue(float value)
        {
            _text.text = value.ToString();
        }
    }
}