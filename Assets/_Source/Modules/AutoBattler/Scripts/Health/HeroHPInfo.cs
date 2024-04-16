using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class HeroHPInfo : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        
        [SerializeField] private Camera _cam;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private Canvas _parentCanvas;
        [SerializeField] private bool _normalize;
        [SerializeField] private HeroCollection _heroCollection;
        [SerializeField] private Text _level;

        private Transform _heroTransform;
        private bool _activated;
        private Health _health;

        internal void Init(Health heroHealth, Transform heroTransform)
        {
            _health = heroHealth;
            _heroTransform = heroTransform;
            _activated = true;
            _health.HealthChanged += UpdateHealthBar;
            if (_heroCollection.IsHeroSelected)
            {
                _level.text = _heroCollection.GetSelectedHero().Level.ToString();
            }
            UpdateHealthBar(1, 1);
            ToggleView(true);
        }

        internal void Reset()
        {
            if (_activated )
            {
                _health.HealthChanged -= UpdateHealthBar;
                ToggleView(false);
                _activated = false;
            }
        }

        internal void ToggleView(bool toggle)
        {
            _canvasGroup.alpha = toggle ? 1 : 0;
        }

        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            _fill.fillAmount = currentHealth / maxHealth;
        }

        private void Update()
        {
            if (_activated == false)
                return;
            var screenPosition = _cam.WorldToScreenPoint(_heroTransform.position);
            var screenSize = _canvasScaler.referenceResolution;
            var screenPositionNormalized = screenPosition * _parentCanvas.transform.localScale.x;
            transform.localPosition = screenPosition - new Vector3(screenSize.x * 0.5f, screenSize.y * 0.5f, 0) + _offset;
            //transform.position = (screenPosition * (_normalize ? _parentCanvas.transform.localScale.x : 1)); 
                                 // - new Vector3(screenSize.x * 0.5f, screenSize.y * 0.5f, 0) + _offset;
        }
    }
}
