using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using System;

namespace Legion
{
    public class BootstrapRoot : MonoBehaviour
    {
        [SerializeField] private YandexManager _yandexManager;
        [SerializeField] private SaveSystemRoot _saveSystem;
        [SerializeField] private VariableSaver _variableSaver;
        [SerializeField] private ConsumationController _consumationController;

        private void Awake()
        {
            _yandexManager.YandexInitialized += LoadSave;
            _saveSystem.OnLoadedSave += HandleConsumation;
            _consumationController.OnConsumingFinished += InitializeVariableSaver;
            _variableSaver.OnVariableSaveInitialized += LoadMainMenu;
        }

        private void LoadMainMenu()
        {
            
            SceneManager.LoadScene(1);
        }

        private void InitializeVariableSaver()
        {
            _variableSaver.Init();
        }

        private void HandleConsumation()
        {
            _consumationController.HandleConsumation(_yandexManager.GetListOfAllItemsPurchased(), out bool consumedSomething);
            if (consumedSomething)
                _saveSystem.Save();
        }

        private void LoadSave()
        {
            if (GameAnalyticsSDK.GameAnalytics.Initialized == false)
            {
                GameAnalyticsSDK.GameAnalytics.Initialize();
            }
            _saveSystem.Load();            
        }

//        private IEnumerator Start()
//        {
            
//#if UNITY_WEBGL && !UNITY_EDITOR
//            yield return YandexGamesSdk.Initialize();
//#endif
//            _saveSystem.Load();
//            _variableSaver.Init();
//            yield return new WaitForEndOfFrame();
//            SceneManager.LoadScene(1);
//        }


    }
}
