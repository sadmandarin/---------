using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroPage
{
    internal class TabButtonImage : TabButtonBase
    {
        [SerializeField] private GameObject _selected;
        [SerializeField] private GameObject _unselected;
        [SerializeField] private GameObject _tabLabel;
        internal override void ToggleSelected(bool isSelected)
        {
            base.ToggleSelected(isSelected);
            _selected.SetActive(isSelected);
            _unselected.SetActive(!isSelected);
            if (_tabLabel != null) _tabLabel.SetActive(isSelected);
        }
    }
}
