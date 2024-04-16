using AutoBattler;
using System;
using UnityEngine;

namespace Legion
{
    internal class MusicController : MonoBehaviour
    {
        [SerializeField] private AutoBattlerAndBoardRoot _battlerRoot;
        [SerializeField] private AutoBattlerRoot _autoBattlerRoot;
        [SerializeField] private AudioClip _battleSound, _mainTheme, _winningSound, _losingSounds, _fightBeginSound;

        private void OnEnable()
        {
            _autoBattlerRoot.OnBattleStarted += ChangeMusicToBattleSound;
            _autoBattlerRoot.OnPlayerWon += PlayWinningSound;
            _autoBattlerRoot.OnPlayerLost += PlayLosingSound;
        }

        private void OnDisable()
        {
            _autoBattlerRoot.OnBattleStarted -= ChangeMusicToBattleSound;
            _autoBattlerRoot.OnPlayerWon -= PlayWinningSound;
            _autoBattlerRoot.OnPlayerLost -= PlayLosingSound;
        }

        internal void PlayLosingSound()
        {
            SoundManager.Instance.StopMusic();
            //SoundManager.Instance.PlaySound(_losingSounds);
            SoundManager.Instance.PlayMusic(_mainTheme, 1.5f);
        }

        private void PlayWinningSound()
        {
            SoundManager.Instance.StopMusic();
            //SoundManager.Instance.PlaySound(_winningSound);
            SoundManager.Instance.PlayMusic(_mainTheme, 1.5f);
        }

        private void ChangeMusicToBattleSound()
        {
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.PlaySound(_fightBeginSound);
            SoundManager.Instance.PlayMusic(_battleSound, 1.5f);
        }
    }
}
