using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PersistentData;

namespace HeroPage
{
    public partial class HeroListPage : MonoBehaviour
    {
        [SerializeField] private Transform _parentForUnlockedItems;
        [SerializeField] private Transform _parentForLockedItems;
        [SerializeField] private HeroListItem _itemPrefab;
        [SerializeField] private List<HeroViewSO> _heroViews;
        [SerializeField] private Text _foundText;
        [SerializeField] private HeroDialog _heroDialogPrefab;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private HeroCollection _heroCollection;
        [SerializeField] ContentFitterRefresh[] _contentFitterRefresh;

        [SerializeField] private RectTransform[] _transformsToRebuild;

        private List<HeroListItem> _items = new List<HeroListItem>();
        private HeroDialog _currentHeroDialog;
        private HeroPageData _currentHero;

        private void Awake()
        {
            if (_heroCollection.CollectionValue.Count == 0)
            {
                _heroCollection.SetData(_heroViews.Select(n => n.HeroPresentation.HeroName.ToString()).ToList());
            }
            UpdateListItems();
        }

        private void OnEnable()
        {
            _heroCollection.CollectionChanged += UpdateListItems;

            StartCoroutine(UpdateLayout());
        }

        private void OnDisable()
        {
            _heroCollection.CollectionChanged -= UpdateListItems;
        }

        public void UpdateListItems()
        {
            foreach (var item in _items)
            {
                Destroy(item.gameObject);
            }
            _items.Clear();
            foreach (var heroData in _heroCollection.CollectionValue)
            {
                var heroView = _heroViews.First(n => n.HeroPresentation.HeroName.ToString() == heroData.Name);
                var item = Instantiate(_itemPrefab, heroData.Unlocked ? _parentForUnlockedItems : _parentForLockedItems);
                HeroPageData heroPageData = new HeroPageData(heroView, heroData.Level, heroData.CollectedShards,
                                                             heroData.Unlocked, heroData.Selected, heroData.IsNew);
                item.SetUp(heroPageData);
                item.ItemPressed += HandleItemPressed;
                item.ToggleLockedVisual(!heroData.Unlocked);
                _items.Add(item);
            }

            var localizedFoundPhrase = Lean.Localization.LeanLocalization.GetTranslationText("Found");
            _foundText.text = localizedFoundPhrase + ": " + _items.Where(n => n.IsLocked == false).Count() + "/" + _heroViews.Count;
            Invoke(nameof(RefreshContentFitters), 0.5f);

        }

        private void RefreshContentFitters()
        {
            foreach (var contentFitter in _contentFitterRefresh)
            {
                contentFitter.RefreshContentFitters();
            }
        }

        private void HandleItemPressed(HeroPageData heroPageData)
        {
            SpawnHeroDialog(heroPageData);
        }

        private void SpawnHeroDialog(HeroPageData heroPageData)
        {
            _currentHero = heroPageData;
            _currentHeroDialog = Instantiate(_heroDialogPrefab);
            _currentHeroDialog.InitCamera(_canvas.worldCamera);
            _currentHeroDialog.SetUp(heroPageData);
            _currentHeroDialog.InitHeroSwitcher(PrevHero, NextHero);
            _currentHeroDialog.HeroDataChanged += UpdateHeroData;
        }

        private void UpdateHeroData(string heroName)
        {
            UpdateListItems();
        }

        private void NextHero()
        {
            Destroy(_currentHeroDialog.gameObject);
            var sortedHeroes = _heroCollection.SortedHeroes();
            var currentHero = sortedHeroes.First(n => n.Name == _currentHero.HeroView.HeroPresentation.HeroName.ToString());
            var currentHeroIndex = sortedHeroes.IndexOf(currentHero);
            var nextHeroIndex = currentHeroIndex + 1> _heroViews.Count - 1 ? 0 : currentHeroIndex + 1;
            var nextHeroData = sortedHeroes[nextHeroIndex];
            var nextHeroView = _heroViews.First(n => n.HeroPresentation.HeroName.ToString() == nextHeroData.Name);
            SpawnHeroDialog(new HeroPageData(nextHeroView, nextHeroData.Level, nextHeroData.CollectedShards, 
                            nextHeroData.Unlocked, nextHeroData.Selected, nextHeroData.IsNew));
        }

        private void PrevHero()
        {
            Destroy(_currentHeroDialog.gameObject);
            var sortedHeroes = _heroCollection.SortedHeroes();
            var currentHero = sortedHeroes.First(n => n.Name == _currentHero.HeroView.HeroPresentation.HeroName.ToString());
            var currentHeroIndex = sortedHeroes.IndexOf(currentHero);
            var prevHeroIndex = currentHeroIndex - 1 < 0 ? _heroViews.Count - 1 : currentHeroIndex - 1;
            var prevHeroData = sortedHeroes[prevHeroIndex];
            var prevHeroView = _heroViews.First(n => n.HeroPresentation.HeroName.ToString() == prevHeroData.Name);
            SpawnHeroDialog(new HeroPageData(prevHeroView, prevHeroData.Level, prevHeroData.CollectedShards,
                            prevHeroData.Unlocked, prevHeroData.Selected, prevHeroData.IsNew));
        }

        private IEnumerator UpdateLayout()
        {
            yield return new WaitForEndOfFrame();
            foreach (var transformToRebuild in _transformsToRebuild)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(transformToRebuild);
            }

            RefreshContentFitters();
        }
    }
}
