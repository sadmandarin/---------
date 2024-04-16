using PersistentData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class SelectHeroButton : MonoBehaviour
    {
        internal Action HeroSelected;

        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonVisual;
        [SerializeField] private HeroCollection _heroCollection;
        [SerializeField] private Sprite _selectHeroSprite, _recallHeroSprite;
        [SerializeField] private GameObject[] _alreadySelectedObjects;
        [SerializeField] private GameObject[] _notSelectedObjects;
        
        private string _heroName;
        private bool _isSelected;

        internal void InitButton(string heroName, bool isLocked, bool isSelected)
        {
            if (isLocked)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _isSelected = isSelected;
                _heroName = heroName;
                ToggleButtonVisual(isSelected);
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleHero);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleHero);
        }

        private void ToggleHero()
        {
            _heroCollection.SelectHero(_heroName);
            ToggleButtonVisual(!_isSelected);
            HeroSelected?.Invoke();
        }

        private void ToggleButtonVisual(bool isSelected)
        {
            _isSelected = isSelected;
            foreach (var selectedObject in _alreadySelectedObjects)
            {
                selectedObject.SetActive(isSelected);
            }
            foreach (var notSelectedObject in _notSelectedObjects)
            {
                notSelectedObject.SetActive(!isSelected);
            }
            _buttonVisual.sprite = isSelected ? _recallHeroSprite : _selectHeroSprite;
        }
    }
}
