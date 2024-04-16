using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class HowToPlay : MonoBehaviour
    {
        public GameObject howToPrefabPrefab;

        public bool isActive = false;

        public GameObject howToPlay;

        [SerializeField]
        private AudioSource audioSource;

        public void ToogleInfo()
        {
            audioSource.Play();

            isActive = !isActive;

            if (isActive)
            {
                howToPlay = Instantiate(howToPrefabPrefab);

                Time.timeScale = 0f;
                audioSource.Stop();
            }
        }
    }
}
