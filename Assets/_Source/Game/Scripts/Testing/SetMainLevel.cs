using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    public class SetMainLevel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private LevelVariable _mainLevel;
        [SerializeField] private InputField _input;

        private void OnEnable()
        {
            _button.onClick.AddListener(SetLevel);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SetLevel);
        }

        private void SetLevel()
        {
            int number = int.Parse(_input.text.ToString());
            _mainLevel.Value = number;
        }
    }
}
