using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TerritoryPage
{
    internal class TerritoryPageItem : MonoBehaviour
    {
        internal Action<GameObject> OnDialogSpawned;
        internal Action<int> OnLockedItemClicked;

        [SerializeField] private TerritoryPageItemData _itemData;
        [SerializeField] private LevelVariable _levelVariable;
        [SerializeField] private GameObject _lock;
        [SerializeField] private Button _button;
        [SerializeField] private Transform _dialogParent;

        private bool _isUnlocked;

        private void OnEnable()
        {
            UpdateView();
            _button.onClick.AddListener(HandleOnClick);
            _levelVariable.OnValueChanged += UpdateView;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnClick);
        }

        internal void HandleOnClick()
        {
            if (_isUnlocked)
            {
                SpawnDialog();
            }
            else
            {
                SpawnTip();
            }
        }

        private void SpawnDialog()
        {
            if (_itemData.DialogPrefab == null)
                return;

            var spawnedDialog = Instantiate(_itemData.DialogPrefab);
            OnDialogSpawned?.Invoke(spawnedDialog);
        }

        private void SpawnTip()
        {
            OnLockedItemClicked?.Invoke(_itemData.LevelRequirement);
        }

        private void UpdateView(int level = 0)
        {
            bool isUnlocked = _itemData.LevelRequirement <= _levelVariable.Value;
            _isUnlocked = isUnlocked;
            if (_lock != null)
                _lock.SetActive(!isUnlocked);
        }
    }
}
