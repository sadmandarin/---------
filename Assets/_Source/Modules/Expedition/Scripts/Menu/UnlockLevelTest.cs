using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Expedition
{
    internal class UnlockLevelTest : MonoBehaviour
    {
        [SerializeField] private ConquestLevelsCollection _levelsCollection;
        [SerializeField] private Dropdown _dropDown;
        [SerializeField] private Button _button;
        [SerializeField] private int _startFromNumber;

        private void OnEnable()
        {
            _dropDown.ClearOptions();
            List<string> options = new List<string>();
            List<Dropdown.OptionData> optionsData = new List<Dropdown.OptionData>();
            for (int i = 1; i < 21; i++)
            {
                options.Add(i.ToString());
                optionsData.Add(new Dropdown.OptionData(i.ToString()));
            }
            _dropDown.AddOptions(options);

            _button.onClick.AddListener(UnlockLevel);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(UnlockLevel);
        }

        private void UnlockLevel()
        {
            int level = _dropDown.value;
            for (int i = level; i > 0; i--)
            {
                if (_levelsCollection.HasFinishedLevel(_startFromNumber + i + 1) == false)
                    _levelsCollection.UnlockLevel(_startFromNumber + i + 1);
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
