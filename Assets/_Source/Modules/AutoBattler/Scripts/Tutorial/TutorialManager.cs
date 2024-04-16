using PersistentData;
using System;
using UnityEngine;

namespace AutoBattler
{
    internal class TutorialManager : MonoBehaviour
    {
        [SerializeField] private LevelVariable _mainLevel;
        [SerializeField] private GameObject[] _objectsToTurnOffOnLevel1;
        [SerializeField] private CombiningTroopsTutorial _combiningTroopsTutorial;
        [SerializeField] private BuyingTroopsTutorial _buyingTroopsTutorial;
        [SerializeField] private VoidEventChannelSO _cardAddedToBoard;
        [SerializeField] private GameObject[] _difficultyObjects;

        private const int LevelWhenDifficultyIsUnlocked = 30;
        private const string PlayerPrefsSaveName = "Tutorial2";

        private void OnEnable()
        {
            if (_mainLevel.Value == 1)
            {
                TurnOffUnnecessaryObjects();
            }
            else
            {
                TurnOnUnnecessaryObjects();
            }

            if (_mainLevel.Value == 2)
            {
                SpawnBuyingTroopsTutorial();
                _cardAddedToBoard.OnEventRaised += SpawnCombiningTroopsTutorial;
            }

            foreach (var difficulty in _difficultyObjects)
            {
                difficulty.SetActive(_mainLevel.Value >= LevelWhenDifficultyIsUnlocked);
            }
            
        }

        private void OnDisable()
        {
            if (_mainLevel.Value == 2)
                _cardAddedToBoard.OnEventRaised -= SpawnCombiningTroopsTutorial;
        }

        private void SpawnCombiningTroopsTutorial()
        {
            if (PlayerPrefs.GetInt(PlayerPrefsSaveName, 0) == 0)
            {
                _combiningTroopsTutorial.SpawnTutorial();
                PlayerPrefs.SetInt(PlayerPrefsSaveName, 1);
            }
            
        }

        private void SpawnBuyingTroopsTutorial()
        {
            Instantiate(_buyingTroopsTutorial);
        }

        private void TurnOffUnnecessaryObjects()
        {
            foreach (var objectToTurnOff in _objectsToTurnOffOnLevel1)
            {
                objectToTurnOff.SetActive(false);
            }
        }

        private void TurnOnUnnecessaryObjects()
        {
            foreach (var objectToTurnOn in _objectsToTurnOffOnLevel1)
            {
                objectToTurnOn.SetActive(true);
            }
        }
    }
}
