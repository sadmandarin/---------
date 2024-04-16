using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SaveSystem
{
    internal static class SaveFileWriter 
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void SyncDB();
#endif
        internal static void WriteSave(string fileName, string data)
        {
            string fullPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName;

            string json = data;
            string encodedJson = CustomEncoding.Base64Encode(json);

#if !UNITY_WEBGL || UNITY_EDITOR
            encodedJson = json;
#endif
            // Local Save
            File.WriteAllText(fullPath, encodedJson);

            // Cloud Save
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.SetCloudSaveData(json, ()=>Debug.Log("Saved to cloud"), (data) => Debug.Log("Couldn't save " + data));
        }
        SyncDB();
#endif
            Debug.Log("succesfully written data" + json);
        }
    }
}
