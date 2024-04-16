using Agava.YandexGames;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    internal static class SaveFileLoader
    {
        internal static bool LoadSaveFromCloud(out string result)
        {
            result = "";
            return false;
#if UNITY_WEBGL && !UNITY_EDITOR
        Debug.Log("if (PlayerAccount.IsAuthorized)" + PlayerAccount.IsAuthorized);
        string dataFromCloud = "";
            bool loaded = false;
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.GetCloudSaveData((loadedSave) => 
                { 
                    dataFromCloud = loadedSave;
                    loaded = true;
                }, (data) =>
                {
                    Debug.LogError("Error when loading from cloud " + data);
                    loaded = false;
                });
                result = dataFromCloud;
                Debug.Log("Loaded from cloud " + dataFromCloud);
                return loaded;
            }
            else
            {
                result = "";
                return false;
            }
#endif
            result = "";
            return false;
        }

        public static bool LoadSaveFromFile(string fileName, out string result)
        {
            string fullPath = Path.Combine(Application.persistentDataPath, fileName);
            Debug.Log("Does save file exists: " + File.Exists(fullPath));
            if (File.Exists(fullPath))
            {
                string text = File.ReadAllText(fullPath);
                string json;
                if (CustomEncoding.IsBase64String(text))
                    json = CustomEncoding.Base64Decode(text);
                else
                    json = text;
                result = json;
                Debug.Log(json);
                return true;
            }
            else
            {
                Debug.Log("Save doesn't exists");
                result = "";
                return false;
            }
        }
    }
}
