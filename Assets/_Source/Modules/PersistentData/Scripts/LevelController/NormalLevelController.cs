using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PersistentData
{
    public class NormalLevelController : MonoBehaviour
    {
        [SerializeField] private IntVariableSO _currentLevel;
        [SerializeField] private int _maxLevel = 250;

        public void NextLevel()
        {
            _currentLevel.Value += 1;
            SceneManager.LoadScene(Constants.HomeSceneIndex);
        }

        public void RepeatLevel()
        {
            SceneManager.LoadScene(Constants.HomeSceneIndex);
        }
    }
}
