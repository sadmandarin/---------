using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PersistentData;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestLevelList : MonoBehaviour
    {
        [SerializeField] private List<ConquestLevelListFlag> _flags;
        [SerializeField] private ConquestLevelsCollection _conquestLevelsCollection;
        [SerializeField] private string _levelPrefix;
        [SerializeField] private GameObject _arrowEffectForNextLevel;
        [SerializeField] private ConquestConfirmDialog _confirmDialog;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private float _focusSpeed = 1;

        private (ConquestLevelListFlag, int) _lastUnlockedFlag = (null, 0);


        private void OnEnable()
        {
            UpdateFlagsViews();
            SubscribeToFlags();
        }

        private void OnDisable()
        {
            UnsubscribeToFlags();
        }

        private void UpdateFlagsViews()
        {
            if (_conquestLevelsCollection.CollectionValue.Count == 0)
                _conquestLevelsCollection.SetUpStartingData();
            for (int i = 0; i < _flags.Count; i++)
            {
                ConquestLevelListFlag flag = _flags[i];
                var levelData = _conquestLevelsCollection.GetLevelData(flag.Level);
                flag.SetUp(levelData.Level, levelData.StarsFilled, levelData.Unlocked, levelData.Finished);
                if (levelData.Level > _lastUnlockedFlag.Item2 && levelData.Unlocked)
                    _lastUnlockedFlag = (flag,  levelData.Level);
            }

            StartCoroutine(SpawnTutorialFlag());
            StartCoroutine(FocusOnLastFlag());
        }

        private IEnumerator SpawnTutorialFlag()
        {
            yield return new WaitForEndOfFrame();
            var unlockedButNotFinishedLevels = _flags.Where(n => n.Unlocked && n.Finished == false).ToList();
            if (unlockedButNotFinishedLevels.Count == 0)
            {
                _arrowEffectForNextLevel.SetActive(false);
                yield break;
            }
            else
            {
                _arrowEffectForNextLevel.SetActive(true);
            }
            var minLevel = unlockedButNotFinishedLevels.Min(n => n.Level);
            var nextFlagThatIsUnlockedButNotFinished = _flags.First(n => n.Level == minLevel);
            if (nextFlagThatIsUnlockedButNotFinished != null)
                _arrowEffectForNextLevel.transform.position = nextFlagThatIsUnlockedButNotFinished.transform.position;
        }

        private IEnumerator FocusOnLastFlag()
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(_scrollRect, _lastUnlockedFlag.Item1.GetComponent<RectTransform>(), 
                _focusSpeed));
        }

        private void SubscribeToFlags()
        {
            foreach (var flag in _flags)
            {
                flag.OnFlagClicked += HandleOnFlagClicked;
            }
        }

        private void UnsubscribeToFlags()
        {
            foreach (var flag in _flags)
            {
                flag.OnFlagClicked -= HandleOnFlagClicked;
            }
        }

        private void HandleOnFlagClicked(int level)
        {
            var dialog = Instantiate(_confirmDialog);
            dialog.InitDialog(_canvas.worldCamera);
            dialog.SetupData(level);
            dialog.OnConfirmedPressed += LoadLevel;
        }

        private void LoadLevel(int level)
        {
            SceneManager.LoadScene(_levelPrefix + level);
        }
    }
}
