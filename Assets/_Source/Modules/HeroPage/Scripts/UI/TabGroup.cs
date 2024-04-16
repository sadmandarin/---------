using System.Collections.Generic;
using UnityEngine;

namespace HeroPage
{
    internal class TabGroup : MonoBehaviour
    {
        [SerializeField] private List<TabButtonBase> _tabButtons;

        private void Awake()
        {
            foreach (var button in _tabButtons)
            {
                button.TabClicked += HandleTabClicked;
            }

            HandleTabClicked(_tabButtons[0]);
        }

        private void HandleTabClicked(TabButtonBase button)
        {
            foreach (var tabButton in _tabButtons)
            {
                if (tabButton == button) 
                    tabButton.ToggleSelected(true);
                else 
                    tabButton.ToggleSelected(false);
            }
        }
    }
}
