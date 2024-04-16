using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class BarracksPage : MonoBehaviour
    {
        [SerializeField] private Transform _contentTransform;
        [SerializeField] private float _yPositionWhenContentIsHidden = -520f;
        [SerializeField] private float _yPositionWhenContentIsShown = 200f;
        [SerializeField] private float _timeToShowBarracks = 1f;
        [SerializeField] private GameObject[] _gameObjectsThatToggleWhenBarracksAreShown;
        [SerializeField] private BarracksCellsManager _cellManager;

        [SerializeField] private Text[] _unitsInBarracksText;
        [SerializeField] private int _maxAmountOfUnits;

        private bool _isShowing;

        private void OnEnable()
        {
            UpdateUnitNumbers(_cellManager.NumberOfUnitsInsideBarracks);
            
        }

        private void OnDisable()
        {
            if (_isShowing)
                Hide();
        }

        internal void Show()
        {
            _isShowing = true;

            _contentTransform.DOLocalMoveY(_yPositionWhenContentIsShown, _timeToShowBarracks);
            
            foreach (var gameObj in _gameObjectsThatToggleWhenBarracksAreShown)
            {
                gameObj.SetActive(!gameObj.activeSelf);
            }
        }

        internal void Hide()
        {
            _isShowing = false;

            _contentTransform.DOLocalMoveY(_yPositionWhenContentIsHidden, _timeToShowBarracks);
            foreach (var gameObj in _gameObjectsThatToggleWhenBarracksAreShown)
            {
                gameObj.SetActive(!gameObj.activeSelf);
            }
        }

        internal void UpdateUnitNumbers(int numberOfUnitsInBarracks)
        {
            foreach (var text in _unitsInBarracksText)
            {
                text.text = string.Format("{0}/<color=white>{1}</color>", numberOfUnitsInBarracks, _maxAmountOfUnits);
            }
        }
    }
}
