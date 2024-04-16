using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using YandexSDK;

namespace Expedition
{
    public class ExitPause : MonoBehaviour
    {
        private PauseManager gameManager;

        public AudioSource audioSource;
        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<PauseManager>();
        }

        public void OnClick()
        {
            if (!gameManager.isPaused) { }
            {
                UnPauseAllAudio();

                gameManager.mainAudioSource.Play();

                Destroy(gameManager._menu);

                gameManager.isPaused = !gameManager.isPaused;

                Time.timeScale = 1;

                ApplicationActivation.Enabled = true;
            }
        }

        void UnPauseAllAudio()
        {
            // ������� ��� ������� � ����������� AudioSource
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

            // ���������������� ������ AudioSource
            foreach (AudioSource audioSource in allAudioSources)
            {
                audioSource.UnPause();
            }
        }
    }
}
