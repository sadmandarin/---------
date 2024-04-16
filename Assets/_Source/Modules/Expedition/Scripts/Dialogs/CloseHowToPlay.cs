using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class CloseHowToPlay : MonoBehaviour
    {
        private HowToPlay gameManager;

        private AudioSource audioSource;
        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<HowToPlay>();

            audioSource = GameObject.Find("Pause").GetComponent<AudioSource>();
        }

        public void OnClick()
        {
            if (!gameManager.isActive) { }
            {
                audioSource.Play();

                Destroy(gameManager.howToPlay);

                Time.timeScale = 1f;

                gameManager.isActive = !gameManager.isActive;
            }
        }
    }
}
