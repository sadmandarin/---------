using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveLanguageSwitcher : MonoBehaviour
{
    [SerializeField] private string ru;
    [SerializeField] private string en;
    [SerializeField] private Text text;

    private string levelLabel;

    private void Awake()
    {
        switch (YandexManager.Instance.Language)
        {
            case "ru":
                levelLabel = ru;
                break;

            case "com":
                levelLabel = en;
                break;
        }
        
        UpdateValue(0);
    }

    public void UpdateValue(int sec)
    {
        text.text = $"{levelLabel} {sec}";
    }    
}
