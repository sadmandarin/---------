using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GridBoard;
using PersistentData;

namespace HeroPage
{
    public class TroopListPage : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private List<TroopViewSO> _troops;
        [SerializeField] private Transform _unlockedParent;
        [SerializeField] private Transform _lockedParent;
        [SerializeField] private RectTransform _verticalLayout;
        [SerializeField] private TroopListItem _prefab;
        [SerializeField] private SelectedTroopTip _selectedTip;
        [SerializeField] private TroopDialogue _dialogue;
        [SerializeField] private Text _foundText;
        [SerializeField] private BoardUnitsPersistentHolder _boardUnitsPersistentHolder;
        [SerializeField] private BarracksUnitsPersistentHolder _barracksUnitsPersistentHolder;

        private List<TroopListItem> _items = new List<TroopListItem>();

        private TroopViewSO _selectedTroop;

        private void Awake()
        {
            SetUp();

            //_selectedButton.onClick.AddListener(ShowDialog);
        }

        private void HandleItemPressed(TroopViewSO troop)
        {
            var dialogue = Instantiate(_dialogue);
            dialogue.SetUp(troop);

            // For Tip placement
            //var newPosition = _items.First(n => n.Troop.name == troop.name).transform;
            //_selectedTroop = troop;
            //_selectedTip.Show(troop.Icon, newPosition);
        }

        public void SetUp()
        {
            ClearItemsList();

            foreach (var item in _troops)
            {
                var doesPlayerHaveThisUnit = _barracksUnitsPersistentHolder.IsUnitInBarracks(item.View.Name.ToString())
                                              || _boardUnitsPersistentHolder.IsUnitOnBoard(item.View.Name.ToString());
                var newItem = Instantiate(_prefab, doesPlayerHaveThisUnit ? _unlockedParent : _lockedParent);
                newItem.SetUp(item);
                newItem.ToggleLockedVisual(!doesPlayerHaveThisUnit);
                _items.Add(newItem);
                newItem.ItemPressed += HandleItemPressed;
            }

            var localizedFoundPhrase = Lean.Localization.LeanLocalization.GetTranslationText("Found");
            _foundText.text = localizedFoundPhrase + ": " + _items.Where(n => n.IsLocked == false).Count() + "/" + _items.Count;
            StartCoroutine(UpdateCanvas());
        }

        private void ClearItemsList()
        {
            foreach (var item in _items)
            {
                Destroy(item.gameObject);
            }
            _items.Clear();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //_selectedTip.Hide();
        }

        private IEnumerator UpdateCanvas()
        {
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_unlockedParent.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(_lockedParent.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(_verticalLayout);
        }

        private void OnEnable()
        {
            StartCoroutine(UpdateCanvas());
        }
    }
}