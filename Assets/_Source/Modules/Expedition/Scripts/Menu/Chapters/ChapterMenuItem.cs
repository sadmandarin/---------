using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Expedition
{
    internal class ChapterMenuItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ConquestLevelsCollection _levelsCollection;
        [SerializeField] private int _fromLevel, _toLevel;
        [SerializeField] private int _mainLevelRequirement;
        [SerializeField] private LevelVariable _mainLevel;
        [SerializeField] private ChapterItemRequirementDescription _description;
        [SerializeField] private GameObject _lockGameObject, _unlockGameObject;
        [SerializeField] private Text _progressText;
        [SerializeField] private Image _progressImage;
        [SerializeField] private string _sceneName;
        [SerializeField] private Image _building, _floor;

        private bool _requirementsFullfilled;

        private void OnEnable()
        {
            UpdateView();
            _button.onClick.AddListener(HandleOnChapterClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnChapterClicked);
        }

        private void HandleOnChapterClicked()
        {
            if (_requirementsFullfilled)
            {
                SceneManager.LoadScene(_sceneName);
            }
            else
            {
                ToggleDescription();
            }
            
        }

        private void ToggleDescription()
        {
            _description.gameObject.SetActive(!_description.gameObject.activeInHierarchy);
        }

        private void UpdateView()
        {
            var starsCollected = _levelsCollection.StarsCollected(_fromLevel, _toLevel);
            int lastLevel = _fromLevel - 1;
            bool finishedLastLevel = _levelsCollection.HasFinishedLevel(lastLevel);
            bool levelRequirementFilled = _mainLevelRequirement <= _mainLevel.Value;
            _description.SetUp(_mainLevelRequirement, lastLevel / 20);
            _requirementsFullfilled = finishedLastLevel && levelRequirementFilled;
            if (_requirementsFullfilled == false)
            {
                _lockGameObject.SetActive(true);
                _unlockGameObject.SetActive(false);
                _building.color = new Color(0.5f,0.5f,0.5f, 1);
                _floor.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                _lockGameObject.SetActive(false);
                _unlockGameObject.SetActive(true);
                _progressText.text = $"{_levelsCollection.StarsCollected(_fromLevel, _toLevel)}/60";
                _progressImage.fillAmount = (float)_levelsCollection.StarsCollected(_fromLevel, _toLevel) / 60f;
                _building.color = new Color(1,1,1, 1);
                _floor.color = new Color(1,1,1, 1);
            }
        }
    }
}
