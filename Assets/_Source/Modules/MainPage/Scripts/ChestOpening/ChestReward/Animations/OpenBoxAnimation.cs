using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    internal class OpenBoxAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _smoke;
        [SerializeField] private AudioSource _boxDownSound;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private ChestCardAnimation _chestCardAnimation;
        [SerializeField] private float _timeToFade;

        private const string Shake = "Shake";

        internal void SpawnSmoke()
        {
            _smoke.Play();
            _boxDownSound.Play();
        }

        internal void ShakeBox()
        {
            _animator.Play(Shake);
        }

        internal void OnBoxShakeEnded()
        {
            _canvasGroup.DOFade(0, _timeToFade);
            _chestCardAnimation.StartAnimation();
        }
    }
}
