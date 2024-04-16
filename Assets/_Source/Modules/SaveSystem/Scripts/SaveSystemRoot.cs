using Agava.YandexGames;
using System;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(menuName = "SaveSystem/SaveSystem")]
    public class SaveSystemRoot : ScriptableObject
    {
        public Action OnLoadedSave;

        [SerializeField] private Save _save;
        [SerializeField] private string _saveFileName;
        [SerializeField] private SaveableVariablesController _variablesController;

        public void Save()
        {
            Save newSave = _variablesController.GetCurrentSave();
            string json = JsonUtility.ToJson(newSave);
            SaveFileWriter.WriteSave(_saveFileName, json);
        }

        public void Load()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.GetCloudSaveData((data) => LoadFromCloud(data), LoadFromFileOrCreateNewSaveFile);
            return;
#endif

            LoadFromFileOrCreateNewSaveFile();
        }

        private void LoadFromCloud(string text)
        {
            if (string.IsNullOrEmpty(text) == false && text != "{}")
            {
                Debug.Log("Loading from cloud " + text);
                ConvertSaveToData(text);
                OnLoadedSave?.Invoke();
            }
            else
            {
                Debug.Log("No save file or save file is empty. Loading from file or creating new save");
                LoadFromFileOrCreateNewSaveFile();
            }
        }

        private void ConvertSaveToData(string text)
        {
            _save = JsonUtility.FromJson<Save>(text);
            _variablesController.LoadSaveIntoData(_save);
        }

        private void LoadFromFileOrCreateNewSaveFile(string text = null)
        {
            bool loadedFromFile = SaveFileLoader.LoadSaveFromFile(_saveFileName, out string savedData);
            if (loadedFromFile)
            {
                Debug.Log("LoadedFromFile " + savedData);
                ConvertSaveToData(savedData);
                OnLoadedSave?.Invoke();
            }
            else
            {
                Debug.Log("No save file ... creating new");
                _save = _variablesController.CreateAndGetEmptySave();
                Save();
                OnLoadedSave?.Invoke();
            }
        }
    }
}
