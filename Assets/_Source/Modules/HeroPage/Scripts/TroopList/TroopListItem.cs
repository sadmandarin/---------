using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HeroPage
{
    internal class TroopListItem : MonoBehaviour, IPointerClickHandler
    {
        internal Action<TroopViewSO> ItemPressed;
        internal TroopViewSO Troop => _troop;
        internal bool IsLocked => _isLocked;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _bg;
        [SerializeField] private Text _name;
        [SerializeField] private Transform _nameParent;
        [SerializeField] private Transform _tipImage;
        [SerializeField] private Color _lockedColor;
        [SerializeField] private Color _unlockedColor;
        [SerializeField] private Sprite[] _troopRarities;

        private TroopViewSO _troop;
        private bool _isLocked;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Pressed");
            ItemPressed?.Invoke(_troop);
        }

        internal void SetUp(TroopViewSO troop)
        {
            _icon.sprite = troop.View.Icon;
            var troopName = troop.View.Name.ToString();
            string localizedName = LeanLocalization.GetTranslationText(troopName);
            
            _name.text = localizedName == null ? _name.text : localizedName.ToString();
            _troop = troop;
            _bg.sprite = _troopRarities[troop.View.Rarity];
        }

        internal void ToggleLockedVisual(bool isLocked)
        {
            _isLocked = isLocked;
            _icon.color = isLocked ? _lockedColor : _unlockedColor;
            _tipImage.gameObject.SetActive(isLocked);
            _nameParent.gameObject.SetActive(!isLocked);
        }
    }
}