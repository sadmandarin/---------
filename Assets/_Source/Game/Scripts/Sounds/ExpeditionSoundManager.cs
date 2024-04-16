using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legion
{
    internal class ExpeditionSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _music;

        private void Awake()
        {
            if (SoundManager.Instance != null )
            {
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(_music);
            }
            
            Time.timeScale = 1f;
        }
    }
}
