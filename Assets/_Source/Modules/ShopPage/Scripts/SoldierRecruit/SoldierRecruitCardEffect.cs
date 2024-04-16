using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopPage
{
    internal class SoldierRecruitCardEffect : MonoBehaviour
    {
        [SerializeField] private float _timeToIncreaseInSize = 1f;
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private AudioSource _getCardAudio;

        private void OnEnable()
        {
            transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            transform.DOScale(1, _timeToIncreaseInSize).SetEase(Ease.Linear).OnComplete(() =>
            {
                _effect.Play();
                _getCardAudio.Play();
            });
        }
    }
}
