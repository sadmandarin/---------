using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Legion
{
    internal class ButtonThatLoadsScene : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _sceneIndexToLoad;

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
            SceneManager.LoadScene(_sceneIndexToLoad);
        }
    }
}
