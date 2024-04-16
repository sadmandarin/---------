using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    public class HeroTopInfo : MonoBehaviour
    {
        public Action OnButtonPressed;

        [SerializeField] private HeroCollection _heroCollection;
        [SerializeField] private Image _upgradeMask, _replaceMask;
        [SerializeField] private Button _upgradeButton, _replaceButton;
        [SerializeField] private HeroLevelUpConfigSO _levelUpConfig;
        [SerializeField] private Color _fadedColor, _fullColor;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject[] _gameObjectsToToggle;

        internal void Move(Vector3 position)
        {
            transform.position = position + _offset;
        }

        internal void ToggleView(bool toggle)
        {
            _canvasGroup.alpha = toggle ? 1 : 0;
            foreach (var go in _gameObjectsToToggle)
            {
                go.SetActive(toggle);
            }
        }

        private void OnEnable()
        {
            if (_heroCollection.IsHeroSelected == false)
            {
                ToggleView(false);
                return;
            }

            //bool isReplaceAvailable = _heroCollection.CollectionValue.Where(n => n.Unlocked).Count
            var heroData = _heroCollection.GetSelectedHero();
            bool upgradeAvailable = heroData.CollectedShards >= _levelUpConfig.GetShardsForUpgrade(heroData.Level);
            ToggleUpgradeButton(upgradeAvailable);

            _upgradeButton.onClick.AddListener(RaiseEvent);
            _replaceButton.onClick.AddListener(RaiseEvent);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(RaiseEvent);
            _replaceButton.onClick.RemoveListener(RaiseEvent);
        }

        internal void RaiseEvent()
        {
            OnButtonPressed?.Invoke();
        }

        private void ToggleUpgradeButton(bool toggle)
        {
            _upgradeMask.color = toggle ? _fullColor : _fadedColor;
            _upgradeButton.enabled = toggle;
        }
    }
}
