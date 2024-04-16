using DG.Tweening;
using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TerritoryPage
{
    internal class TransitionToExpeditionDialog : MonoBehaviour
    {
        [SerializeField] private Image[] _clouds;
        [SerializeField] private Animator _cloudAnimator;
        [SerializeField] private float _timeAfterMovingToNextScene;
        [SerializeField] private ConquestLevelsCollection _levelsCollection;
        [SerializeField] private LevelVariable _mainLevel;

        private const string CloudsIn = "c_in";
        private const int ExpeditionSceneIndex = 3;

        private const string ExpeditionSceneChapter1Name = "ExpeditionChapter1";
        private const string ExpeditionSceneChapter2Name = "ExpeditionChapter2";

        private void Awake()
        {
            _cloudAnimator.Play(CloudsIn);
            foreach (var cloud in _clouds)
            {
                cloud.DOFade(1, _timeAfterMovingToNextScene);
            }
            Invoke(nameof(LoadExpeditionScene), _timeAfterMovingToNextScene);
        }

        private void LoadExpeditionScene()
        {
            if (_levelsCollection.GetLevelData(20).Finished && _mainLevel.Value >= 80)
            {
                SceneManager.LoadScene(ExpeditionSceneChapter2Name);
            }
            else
            {
                SceneManager.LoadScene(ExpeditionSceneChapter1Name);
            }
            
        }
    }
}
