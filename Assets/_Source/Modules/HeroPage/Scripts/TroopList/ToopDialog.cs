using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class TroopDialogue : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _levelImage;
        [SerializeField] private Image _bgImage;
        [SerializeField] private Sprite[] _levelsSprites;
        [SerializeField] private Sprite[] _bgSprites;
        [SerializeField] private Text _description;
        [SerializeField] private GameObject[] _objectForSkillsPage;
        [SerializeField] private SkillsTroopPage _skillPage;

        [SerializeField] private UnlockingRequirementStat _unlockingRequirementStat;
        [SerializeField] private BasicStats  _basicStats;
        [SerializeField] private DamageShapeStat _damageShapeStat;
        [SerializeField] private DamageTypeStat _damageTypeStat;
        [SerializeField] private UnitQuantityStat _unitQuantityStat;
        [SerializeField] private DamageRangeStat _damageRangeStat;
        [SerializeField] private TargetSelectionStat _targetSelectionStat;
        [SerializeField] private AttackDistanceAndTypeStat _attackDistanceAndTypeStat;

        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private UnitAdder _unitAdder;

        private TroopViewSO _selectedTroop;
        private int _selectedLevel = 1;
        private const int MinLevel = 1;

        internal void SetUp(TroopViewSO troop, int level = 1)
        {
            _selectedTroop = troop;
            _name.text = Lean.Localization.LeanLocalization.GetTranslationText(troop.View.Name.ToString());
            _icon.sprite = troop.View.Icon;
            ChangeStats(troop, level);
            SetUpSkillsPage(troop);
        }

        private void SetUpSkillsPage(TroopViewSO troop)
        {
            foreach (var skillsPageObject in _objectForSkillsPage)
            {
                skillsPageObject.SetActive(troop.HasSkill);
            }

            if (troop.HasSkill)
            {
                _skillPage.SetUp(troop.SkillView.Icon, troop.SkillView.Title, troop.SkillView.Description);
            }

        }

        private void ChangeStats(TroopViewSO troop, int level)
        {
            SetDescription(troop.Description);
            SetBackground(troop.View.Rarity);
            SetLevel(level);
            _unlockingRequirementStat.Set(!_unitAdder.HasUnit(troop.View.Name.ToString()), troop.UnlockedAtLevel);
            _basicStats.SetStats(troop.Stats.StatsLevels[level - 1].Damage, troop.Stats.StatsLevels[level - 1].Health, troop.Stats.StatsLevels[level - 1].Defense,
                     troop.Stats.StatsLevels[level - 1].MovementSpeed, troop.Stats.StatsLevels[level - 1].AttackSpeed);
            _unitQuantityStat.Set(troop.IsSingleCount, level);
            _damageTypeStat.Set(troop.IsMagicAttacker);
            _attackDistanceAndTypeStat.Set(troop.AttackType == TypeOfAttack.Melee, troop.AttackDistance);
            SetDamageRange(troop.Stats.StatsLevels[level - 1].HitRadius);
            _damageShapeStat.Set(troop.DamageShape);
            _targetSelectionStat.Set(troop.TargetSelection);
            if (troop.HasSkill)
                _skillPage.ChangeSkillDescription(troop.SkillView, level);
        }

        private void SetDamageRange(float range)
        {
            if (range == 0)
                _damageRangeStat.gameObject.SetActive(false);
            else
            {
                _damageRangeStat.gameObject.SetActive(true);
                _damageRangeStat.Set(range);
            }
        }

        private void SetDescription(string localizedPhraseName)
        {
            _description.text = Lean.Localization.LeanLocalization.GetTranslationText(localizedPhraseName);
        }

        private void SetBackground(int rarity)
        {
            _bgImage.sprite = _bgSprites[rarity];
        }

        private void SetLevel(int level)
        {
            _levelImage.sprite = _levelsSprites[level - 1];
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
            _nextButton.onClick.AddListener(NextLevel);
            _prevButton.onClick.AddListener(PreviousLevel);
            
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);
            _nextButton.onClick.RemoveListener(NextLevel);
            _prevButton.onClick.RemoveListener(PreviousLevel);
        }

        private void Close()
        {
            Destroy(gameObject);
        }

        private void NextLevel()
        {
            ChangeLevel(_selectedLevel + 1);
        }

        private void PreviousLevel()
        {
            ChangeLevel(_selectedLevel - 1);
        }

        private void ChangeLevel(int level)
        {
            if (level > _selectedTroop.Stats.StatsLevels.Count())
                _selectedLevel = MinLevel;
            else if (level < MinLevel)
                _selectedLevel = _selectedTroop.Stats.StatsLevels.Count();
            else
                _selectedLevel = level;
            ChangeStats(_selectedTroop, _selectedLevel);
        }
    }
}