using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestDialog : MonoBehaviour
    {
        internal Action ChestDialogClosed;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _closeButton;
        [SerializeField] private List<ChestDialogChestItem> _chestItems;
        [SerializeField] private List<ChestVariableSO> _chestVariables;

        internal void Init(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
            UpdateChests();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseDialog);
            foreach (var chest in _chestVariables)
            {
                chest.ChestQuantityChanged += UpdateChests;
            }
        }

        private void OnDisable()
        {
            foreach (var chest in _chestVariables)
            {
                chest.ChestQuantityChanged -= UpdateChests;
            }
        }

        private void CloseDialog()
        {
            ChestDialogClosed?.Invoke();
            Destroy(gameObject);
        }

        private void UpdateChests()
        {
            for (int i = 0; i < _chestItems.Count; i++)
            {
                var chestData = _chestVariables[i];
                _chestItems[i].SetUp(chestData);
            }
        }
    }
}
