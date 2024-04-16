using DG.Tweening;
using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    public class ItemCollectionAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private GameObject _gemPrefab;
        [SerializeField] private int _numberOfCoins;
        [SerializeField] private float _offsetMultiplier;
        [SerializeField] private Transform _coinParent;
        [SerializeField] private float _timeToMoveToTheSide;
        [SerializeField] private float _timeToMoveToCoinsTab;
        [SerializeField] private Transform _coinsTab;
        [SerializeField] private Transform _gemsTab;
        [SerializeField] private float _delayBetweenCoins;
        [SerializeField] private float _scaleToRelativelyIncreaseTo;
        [SerializeField] private float _scaleToRelativelyShrinkTo;
        [SerializeField] private AudioSource _gemsAppear;
        [SerializeField] private AudioSource _coinsAppear;
        [SerializeField] private AudioSource _coinCollect;
        [SerializeField] private VoidEventChannelSO _onCollectingCoins, _onCollectingGems;

        public void PlayAnimations(bool playGems, bool playCoins)
        {
            if(playGems && playCoins)
            {
                StartCoroutine(StartCoinAnimation());
                StartCoroutine(StartGemsAnimation());
            }
            else if (playGems && !playCoins)
                StartCoroutine(StartGemsAnimation());
            else if (playCoins)
                StartCoroutine(StartCoinAnimation());
            else
                return;
        }

        private IEnumerator StartCoinAnimation()
        {
            _coinsAppear.Play();
            for (int i = 0; i < _numberOfCoins; i++)
            {
                var coin = Instantiate(_coinPrefab, _coinParent);
                var offset = Random.insideUnitCircle * _offsetMultiplier;
                DOTween.Sequence().Append(coin.transform.DOMove(offset, _timeToMoveToTheSide).SetRelative(true))
                                  .Join(coin.transform.DOScale(_scaleToRelativelyIncreaseTo, _timeToMoveToTheSide).SetRelative(true))
                                  .Append(coin.transform.DOMove(_coinsTab.position, _timeToMoveToCoinsTab).SetEase(Ease.Linear))
                                  .Join(coin.transform.DOScale(-_scaleToRelativelyShrinkTo, _timeToMoveToTheSide).SetRelative(true))
                                  .AppendCallback(() =>
                                  {
                                      Destroy(coin.gameObject);
                                      _coinCollect.Play();
                                  });
                yield return new WaitForSeconds(_delayBetweenCoins);
            }
        }

        private IEnumerator StartGemsAnimation()
        {
            _gemsAppear.Play();
            for (int i = 0; i < _numberOfCoins; i++)
            {
                var gem = Instantiate(_gemPrefab, _coinParent);
                var offset = Random.insideUnitCircle * _offsetMultiplier;
                DOTween.Sequence().Append(gem.transform.DOMove(offset, _timeToMoveToTheSide).SetRelative(true))
                                  .Join(gem.transform.DOScale(_scaleToRelativelyIncreaseTo, _timeToMoveToTheSide).SetRelative(true))
                                  .Append(gem.transform.DOMove(_gemsTab.position, _timeToMoveToCoinsTab))
                                  .Join(gem.transform.DOScale(-_scaleToRelativelyShrinkTo, _timeToMoveToTheSide).SetRelative(true))
                                  .AppendCallback(() => Destroy(gem.gameObject));
                yield return new WaitForSeconds(_delayBetweenCoins);
            }
        }

        private void OnEnable()
        {
            if (_onCollectingCoins != null)
                _onCollectingCoins.OnEventRaised += PlayCoinsAnimation;
            if (_onCollectingGems != null)
                _onCollectingGems.OnEventRaised += PlayGemsAnimation;
        }

        private void OnDisable()
        {
            if (_onCollectingCoins != null)
                _onCollectingCoins.OnEventRaised -= PlayCoinsAnimation;
            if (_onCollectingGems != null)
                _onCollectingGems.OnEventRaised -= PlayGemsAnimation;
        }

        private void PlayGemsAnimation()
        {
            PlayAnimations(true, false);
        }

        private void PlayCoinsAnimation()
        {
            PlayAnimations(false, true);
        }
    }
}
