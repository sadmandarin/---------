using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnitsData;
using static HeroPage.HeroListPage;

namespace HeroPage
{
    internal class HeroListItem : MonoBehaviour, IPointerClickHandler
    {
        internal Action<HeroPageData> ItemPressed;
        internal bool IsLocked => _isLocked;
        internal string HeroName => _heroPageData.HeroView.HeroPresentation.HeroName.ToString();

        [SerializeField] private Image _icon;
        [SerializeField] private Transform _tipImage;
        [SerializeField] private Color _lockedColor;
        [SerializeField] private Color _unlockedColor;
        [SerializeField] private Sprite[] _troopRarities;
        [SerializeField] private Image _bg;
        [SerializeField] private Text _levelText;
        [SerializeField] private Text _shardsText;
        [SerializeField] private Image _shardsProgress;
        [SerializeField] private GameObject[] _selectedObjects;
        [SerializeField] private GameObject[] _newCardPanel;
        [SerializeField] private HeroLevelUpConfigSO _levelUpConfig;

        private bool _isLocked;
        private HeroPageData _heroPageData;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Pressed");
            ItemPressed?.Invoke(_heroPageData);
        }

        internal void SetUp(HeroPageData heroPageData)
        {
            _heroPageData = heroPageData;
            _icon.sprite = _heroPageData.HeroView.HeroPresentation.Icon;
            _bg.sprite = _troopRarities[_heroPageData.HeroView.HeroPresentation.Rarity];
            foreach (var selectedObject in _selectedObjects)
            {
                selectedObject.SetActive(heroPageData.IsSelected);
            }
            foreach (var newObject in _newCardPanel)
            {
                newObject.SetActive(heroPageData.IsNew);
            }
            _levelText.text = heroPageData.Level.ToString();
            FillShardsData(heroPageData.Level, heroPageData.Shards);
        }

        internal void ToggleLockedVisual(bool isLocked)
        {
            _isLocked = isLocked;
            _icon.color = isLocked ? _lockedColor : _unlockedColor;
            _tipImage.gameObject.SetActive(isLocked);
        }


        private void FillShardsData(int level, int shards)
        {
            if (level >= _levelUpConfig.MaxLevel)
            {
                _shardsText.text = "MAX";
            }
            else
            {
                var shardsCost = _levelUpConfig.GetShardsForUpgrade(level);
                _shardsText.text = shards + "/" + shardsCost;
                var shardsProgressScale = _shardsProgress.transform.localScale;
                shardsProgressScale.x = Mathf.Clamp((float)shards / (float)shardsCost, 0, 1);
                _shardsProgress.transform.localScale = shardsProgressScale;
            }
        }
    }
}
