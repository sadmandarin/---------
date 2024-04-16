using DG.Tweening;
using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    internal class ExperienceCollection : MonoBehaviour
    {
        [SerializeField] private IntEventChannelSO _onExperienceAdded;
        [SerializeField] private GameObject _experiencePrefab;
        [SerializeField] private Transform _startingPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _textYOffset;
        [SerializeField] private float _randomness;
        [SerializeField] private FloatingAndDisappearingText _disappearingText;
        [SerializeField] private Transform _textParent;
        [SerializeField] private Transform _experienceParent;
        [SerializeField] private float _delayBetweenCoins;
        [SerializeField] private float _scaleToRelativelyIncreaseTo;
        [SerializeField] private float _scaleToRelativelyShrinkTo;
        [SerializeField] private float _timeToMoveToTheSide;
        [SerializeField] private float _timeToMoveToCoinsTab;

        private void OnEnable()
        {
            _onExperienceAdded.OnEventRaised += SpawnExperience;
        }

        private void OnDisable()
        {
            _onExperienceAdded.OnEventRaised -= SpawnExperience;
        }

        private void SpawnExperience(int experience)
        {
            var disappearingText = Instantiate(_disappearingText, _textParent);
            disappearingText.SetText("+" + experience);
            StartCoroutine(StartSpawningExperienceItems(experience));
        }

        private IEnumerator StartSpawningExperienceItems(int numberOfItems)
        {
            for (int i = 0; i < Mathf.Clamp(numberOfItems, 1, 9); i++)
            {
                var experienceItem = Instantiate(_experiencePrefab, _experienceParent);
                var offset = UnityEngine.Random.insideUnitSphere * _randomness;
                DOTween.Sequence().Append(experienceItem.transform.DOMove(offset, _timeToMoveToTheSide).SetRelative(true))
                                  .Join(experienceItem.transform.DOScale(_scaleToRelativelyIncreaseTo, _timeToMoveToTheSide).SetRelative(true))
                                  .Append(experienceItem.transform.DOMove(_endPoint.position, _timeToMoveToCoinsTab).SetEase(Ease.Linear))
                                  .Join(experienceItem.transform.DOScale(-_scaleToRelativelyShrinkTo, _timeToMoveToTheSide).SetRelative(true))
                                  .AppendCallback(() =>
                                  {
                                      Destroy(experienceItem.gameObject);
                                  });
                yield return new WaitForSeconds(_delayBetweenCoins);
            }
        }
    }
}
