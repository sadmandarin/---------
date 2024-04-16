using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroDialog : MonoBehaviour
    {
        internal Action<string> HeroDataChanged;

        [SerializeField] private Text _nameText;
        [SerializeField] private Text _rarityText;
        [SerializeField] private Image _rarityImage;
        [SerializeField] private Sprite[] _raritiesSprites;
        [SerializeField] private LeanPhrase[] _raritiesNames;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _parentToSpawnHero;
        [SerializeField] private Transform _parentToSpawnSkills;
        [SerializeField] private HeroStatsDescriptionBubble _heroStatsDescriptionBubble;
        [SerializeField] private HeroStatsBasicValues _basicStats;
        [SerializeField] private HeroSkillItem _skillPrefab;
        [SerializeField] private HeroDialogTipController _tipController;
        [SerializeField] private HeroDialogSwitcher _heroDialogSwitcher;
        [SerializeField] private HeroDialogLevelContainer _levelContainer;
        [SerializeField] private SelectHeroButton _selectHeroButton;
        [SerializeField] private HeroDialogUpgradeLevelButton _upgradeButton;
        [SerializeField] private HeroOriginButton _originButton;
        [SerializeField] private HeroOriginButton _originButtonInProgress;
        [SerializeField] private HeroOriginDialogSpawner _originSpawner;
        [SerializeField] private HeroRaceAndRoleView _raceAndRole;

        private List<HeroSkillItem> _skills = new List<HeroSkillItem>();
        private HeroPageData _heroPageData;

        internal void InitCamera(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }

        internal void SetUp(HeroPageData heroPageData)
        {
            _heroPageData = heroPageData;
            var level = heroPageData.Level;
            _nameText.text = heroPageData.HeroView.Title;
            _rarityImage.sprite = _raritiesSprites[heroPageData.HeroView.HeroPresentation.Rarity];
            _rarityText.text = LeanLocalization.GetTranslationText(_raritiesNames[heroPageData.HeroView.HeroPresentation.Rarity].name);
            var stats = heroPageData.HeroView.Stats;
            UnitStats unitStats = stats.StatsLevels[level - 1];
            _heroStatsDescriptionBubble.SetUp(stats.StatsLevels.Select(n => n.Health).ToArray(),
                                              stats.StatsLevels.Select(n => n.Defense).ToArray(),
                                              stats.StatsLevels.Select(n => n.Damage).ToArray(),
                                              level - 1);
            _tipController.AddTipBubble((ITipBubble)_heroStatsDescriptionBubble);
            SetSkills(heroPageData.HeroView.Skills, level);
            _basicStats.Set(unitStats.Health, unitStats.Defense, unitStats.Damage, heroPageData.HeroView.IsMagicAttacker);

            _levelContainer.SetData(level, heroPageData.Shards);
            _levelContainer.gameObject.SetActive(heroPageData.IsUnlocked);
            _originSpawner.Init(_parentToSpawnHero.gameObject);

            var defaultHeroName = heroPageData.HeroView.HeroPresentation.HeroName.ToString();
            _selectHeroButton.InitButton(defaultHeroName, !heroPageData.IsUnlocked, heroPageData.IsSelected);
            _selectHeroButton.HeroSelected += HandleHeroSelected;

            if (heroPageData.IsUnlocked)
            {
                _upgradeButton.Init(level, defaultHeroName, heroPageData.Shards);
                _upgradeButton.HeroUpgraded += HandleHeroUpgraded;
            }

            _upgradeButton.gameObject.SetActive(heroPageData.IsUnlocked);
            _originButton.gameObject.SetActive(heroPageData.IsUnlocked == false);

            var heroData = heroPageData.HeroView;
            _raceAndRole.Init(heroData.Race.Description, heroData.Race.Icon, heroData.Role.Description, heroData.Role.Icon);

            Instantiate(heroPageData.HeroView.Prefab, _parentToSpawnHero);
        }

        private void HandleHeroSelected()
        {
            HeroDataChanged?.Invoke(_heroPageData.HeroView.HeroPresentation.HeroName.ToString());
        }

        internal void InitHeroSwitcher(Action prevHero, Action nextHero)
        {
            _heroDialogSwitcher.PressedNextHero += nextHero;
            _heroDialogSwitcher.PressedPrevHero += prevHero;
        }

        private void SetSkills(HeroSkillViewSO[] skills, int level)
        {
            ClearSkills();
            foreach (var skill in skills)
            {
                var skillItem = Instantiate(_skillPrefab, _parentToSpawnSkills);
                skillItem.SetUp(skill.Icon, skill.Title, skill.IsPassive, skill.Description, skill.SkillDescriptions, level, skill.DamageType);
                _skills.Add(skillItem);
                _tipController.AddTipBubble((ITipBubble)skillItem);
            }
        }

        private void ClearSkills()
        {
            foreach (var item in _skills)
            {
                _tipController.RemoveTipBubble(item);
                Destroy(item.gameObject);
            }
            _skills.Clear();
        }

        private void HandleHeroUpgraded()
        {
            var nextLevelHero = new HeroPageData(_heroPageData.HeroView, _heroPageData.Level + 1, 
                _heroPageData.Shards - _heroPageData.Level - 1, _heroPageData.IsUnlocked, 
                _heroPageData.IsSelected, _heroPageData.IsSelected);
            Destroy(_parentToSpawnHero.GetChild(0).gameObject);
            SetUp(nextLevelHero);
            HeroDataChanged?.Invoke(_heroPageData.HeroView.HeroPresentation.HeroName.ToString());
        }
    }
}
