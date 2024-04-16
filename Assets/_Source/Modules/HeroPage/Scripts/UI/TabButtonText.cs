using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class TabButtonText : TabButtonBase
    {
        [SerializeField] private Text _tabTitle;
        
        [SerializeField] private GameObject _activeObject;
        [SerializeField] private Color _activeTextColor;
        [SerializeField] private Color _inactiveTextColor;

        internal override void ToggleSelected(bool isSelected)
        {
            base.ToggleSelected(isSelected);
            _tabTitle.color = isSelected ? _activeTextColor : _inactiveTextColor;
            _activeObject.SetActive(isSelected);
        }
    }
}
