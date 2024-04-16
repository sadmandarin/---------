using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Legion
{
    public class SoundManager : PersistentSingleton<SoundManager>
    {
        [SerializeField] private AudioSource _musicSource, _effectsSource;

        private float _startingMusicSourceVolume;
        private float _startingEffectsSourceVolume;

        private bool _isMusicPlaying = true;
        private AudioClip _musicClipThatIsPlaying;

        protected override void Awake()
        {
            base.Awake();

            _startingEffectsSourceVolume = _effectsSource.volume;
            _startingMusicSourceVolume = _musicSource.volume;
        }
        public void PlaySound(AudioClip clip)
        {
            //_effectsSource.clip = clip;
            //_effectsSource.Play();
            _effectsSource.PlayOneShot(clip);
        }

        public void StopSound()
        {
            _effectsSource.Stop();
        }

        internal void TurnOffSound()
        {
            AudioListener.volume = 0;
            _effectsSource.volume = 0;
            _musicSource.volume = 0;
        }

        internal void TurnOnSound()
        {
            AudioListener.volume = 1;
            _effectsSource.volume = _startingEffectsSourceVolume;
            _musicSource.volume = _startingMusicSourceVolume;
        }

        internal void PlayMusic(AudioClip clip, float delay = 0)
        {
            _isMusicPlaying = true;
            _musicClipThatIsPlaying = clip;
            StartCoroutine(PlayDelayedMusic(clip, delay));
        }

        internal void StopMusic()
        {
            _musicSource.Stop();
            _isMusicPlaying = false;
        }

        private IEnumerator PlayDelayedMusic(AudioClip clip, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            if (_isMusicPlaying == false || _musicClipThatIsPlaying != clip)
                yield break;
            _musicSource.clip = clip;
            _musicSource.Play();
        }
    }
}