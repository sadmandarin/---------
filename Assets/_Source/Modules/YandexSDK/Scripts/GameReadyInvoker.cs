using UnityEngine;

internal class GameReadyInvoker : MonoBehaviour
{
    internal GameReadyInvoker Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (YandexManager.Instance != null)
                YandexManager.Instance.InvokeGameReadyAPI();
        }
        else
            Destroy(gameObject);
    }


}
