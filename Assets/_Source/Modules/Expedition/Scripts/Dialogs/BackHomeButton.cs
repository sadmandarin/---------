using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Expedition
{
    public class BackHomeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private const int _chapter1Index = 3;
        private const int _chapter2Index = 4;

        private int _sceneIndex = _chapter1Index;

        internal void SetUp(int level)
        {
            if (level > 20)
            {
                _sceneIndex = _chapter2Index;
            }
            else
            {
                _sceneIndex = _chapter1Index;
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadScene);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
