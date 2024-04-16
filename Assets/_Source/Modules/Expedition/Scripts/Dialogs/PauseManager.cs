using Expedition;
using System.Data;
using UnityEditor;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject menuPrefab;

    public bool isPaused = false;

    private int level;

    
    private PauseAndGameEndDialog endDialog;

    public GameObject _menu;

    public AudioSource mainAudioSource;

    private GameManager gameManager;

    private void SetUpData()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        level = gameManager.level;

        endDialog.SetupData(level);
    }

    public void TogglePause()
    {
        AudioSource audioSource = GameObject.Find("Pause").gameObject.GetComponent<AudioSource>();

        audioSource.Play();
        
        isPaused = !isPaused;

        YandexSDK.ApplicationActivation.Enabled = !isPaused;

        if (isPaused)
        {
            _menu = Instantiate(menuPrefab);
            endDialog = _menu.GetComponent<PauseAndGameEndDialog>();
            SetUpData();
            PauseAllAudio();

            // Поставить игру на паузу
            Time.timeScale = 0;
        }
    }

    void PauseAllAudio()
    {
        // Находим все объекты с компонентом AudioSource
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        // Приостанавливаем каждый AudioSource
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != mainAudioSource)
            {
                audioSource.Pause();
            }
            
        }
    }
}
