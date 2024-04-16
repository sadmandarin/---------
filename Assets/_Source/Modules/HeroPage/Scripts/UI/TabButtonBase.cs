using System;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal abstract class TabButtonBase : MonoBehaviour
    {
        internal Action<TabButtonBase> TabClicked;

        [SerializeField] private Button _button;
        [SerializeField] private GameObject _content;

        internal virtual void ToggleSelected(bool isSelected)
        {
            _content.SetActive(isSelected);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ClickTab);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ClickTab);
        }

        private void ClickTab()
        {
            TabClicked?.Invoke(this);
        }
    }
}
