using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroDialogSwitcher : MonoBehaviour
    {
        internal Action PressedNextHero, PressedPrevHero;

        [SerializeField] private Button _nextButton, _prevButton;
        [SerializeField] private Transform _heroParent;
        [SerializeField] private float _amountToMoveHero;

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(NextHero);
            _prevButton.onClick.AddListener(PrevHero);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(NextHero);
            _prevButton.onClick.RemoveListener(PrevHero);
        }

        private void NextHero()
        {
            _heroParent.transform.DOMoveX(_heroParent.transform.position.x - _amountToMoveHero, 0.5f).
                OnComplete(() => PressedNextHero?.Invoke());
        }

        private void PrevHero()
        {
            _heroParent.transform.DOMoveX(_heroParent.transform.position.x + _amountToMoveHero, 0.5f).
                OnComplete(() => PressedPrevHero?.Invoke());
        }
    }

}
