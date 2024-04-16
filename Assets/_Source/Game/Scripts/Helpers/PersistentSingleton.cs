using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legion
{
    public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null) Instance = this as T;
            else Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}