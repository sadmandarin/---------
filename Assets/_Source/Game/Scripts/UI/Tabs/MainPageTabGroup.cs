using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    public partial class MainPageTabGroup : MonoBehaviour
    {
        [SerializeField] private TabContent[] _tabContents;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private MainPageTabButtonUI[] _tabButtons;
        [SerializeField] private Image _activeImageEffect;
        [SerializeField] private Vector2 _activeImageEffectIncreaseInSize;
        [SerializeField] private AudioClip _tabSound;

        private int _lastSelectedTab = -1;
        private int _selectedTab = -1;

        private void Start()
        {
            foreach (var tab in _tabButtons)
            {
                tab.TabSelected += SelectTab;
            }
            SelectTab(2);
        }

        public void SelectTab(int indexOfSelectedTab)
        {
            DeselectAllTabButtons();
            SelectTabButton(indexOfSelectedTab);
            SoundManager.Instance.PlaySound(_tabSound);
            ActivateTabContent(indexOfSelectedTab);
        }

        private void DeselectAllTabButtons()
        {
            foreach (var tabButton in _tabButtons) tabButton.Deselect();
        }

        private void SelectTabButton(int indexOfSelectedTab)
        {
            _tabButtons[indexOfSelectedTab].Select();
            _activeImageEffect.transform.position = new Vector3(_tabButtons[indexOfSelectedTab].transform.position.x, _activeImageEffect.transform.position.y,
                                                                _activeImageEffect.transform.position.z);
            Vector2 newSizeDelta = _tabButtons[indexOfSelectedTab].GetComponent<RectTransform>().sizeDelta +
                                                         _activeImageEffectIncreaseInSize;
            newSizeDelta.y = _activeImageEffect.rectTransform.sizeDelta.y;
            _activeImageEffect.rectTransform.sizeDelta = newSizeDelta;
        }

        private void ActivateTabContent(int indexOfSelectedTab)
        {
            if (/*_lastSelectedTab == indexOfSelectedTab ||*/ _selectedTab == indexOfSelectedTab)
                return;

            _tabContents[indexOfSelectedTab].Show();
            _scrollRect.content.transform.DOKill();
            _selectedTab = indexOfSelectedTab;

            if (_lastSelectedTab == -1)
            {
                _scrollRect.content.transform.DOLocalMoveX(-540 - 1080 * indexOfSelectedTab, 0.5f);
                _lastSelectedTab = indexOfSelectedTab;
            }
            else
            {
                _tabContents[_lastSelectedTab].HideCamera();
                _scrollRect.content.transform.DOLocalMoveX(-540 - 1080 * indexOfSelectedTab, 0.5f)
                .OnComplete(() =>
                {
                    _tabContents[_lastSelectedTab].Hide();
                    _tabContents[indexOfSelectedTab].ShowCamera();
                    _lastSelectedTab = indexOfSelectedTab;
                }).OnKill(() =>
                {
                    _lastSelectedTab = indexOfSelectedTab;
                });
            }
        }
    }
}