using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroSkillItem : MonoBehaviour, ITipBubble
    {
        [SerializeField] private Button _button;
        [SerializeField] private CanvasGroup _descriptionCanvasGroup;
        [SerializeField] private Image _skillIcon;
        [SerializeField] private Text _skillTitle;
        [SerializeField] private Text _skillTypeText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Text _levelText;
        [SerializeField] private Transform _parentToSpawnUpgradeInfo;
        [SerializeField] private GameObject _descriptionParent;
        [SerializeField] private HeroSkillItemUpgradeInfo _upgradeInfoPrefab;
        [SerializeField] private LeanPhrase _activeSkillPhrase, _passiveSkillPhrase;
        [SerializeField] private HeroSkillDamageType _skillDamageType;

        public bool IsActive { get; private set; }

        public Action<ITipBubble> TipShown { get; set; }

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleDescription);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleDescription);
        }

        private void ToggleDescription()
        {
            if (IsActive)
                Hide();
            else
                Show();
        }

        internal void SetUp(Sprite icon, string title, bool isPassive, string descriptionText, HeroSkillDescription[] skillDescriptions,
                            int level, SkillDamageType damageType)
        {
            _skillIcon.sprite = icon;
            _skillTitle.text = "<b>" + title + "</b>";
            string typeOfSkill = "<b>" + 
                LeanLocalization.GetTranslationText(isPassive ? _passiveSkillPhrase.name : _activeSkillPhrase.name)
                + "</b>";
            _skillTypeText.text = typeOfSkill;
            _descriptionText.text = descriptionText;
            _levelText.text = "LVL " + level.ToString();

            _skillDamageType.SetUp(damageType);

            foreach (var skillDescription in skillDescriptions)
            {
                var descriptionItem = Instantiate(_upgradeInfoPrefab, _parentToSpawnUpgradeInfo);
                descriptionItem.Set(skillDescription, level - 1);
            }
        }

        public void Show()
        {
            IsActive = true;
            _descriptionParent.SetActive(true);
            TipShown?.Invoke(this);
        }

        public void Hide()
        {
            IsActive = false;
            _descriptionParent.SetActive(false);
        }
    }
}
