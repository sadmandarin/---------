using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Legion
{
    internal class ButtonThatLoadsMainScene : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _sceneIndexToLoad;
        [SerializeField] private GameObject _clouds;

        private bool _buttonPressed;

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
            if (_buttonPressed)
                return;
            _buttonPressed = true;
            _clouds.SetActive(true);
            SceneManager.LoadSceneAsync(_sceneIndexToLoad);
            
        }
    }
}
