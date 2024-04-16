using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MysticStore
{
    internal class MysticStoreDetailsDialog : MonoBehaviour
    {
        internal Action<MysticRewardItemBase> OnItemPurchased;

        [SerializeField] private MysticStoreItemVisual _itemVisual;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Text _priceText;
        [SerializeField] private Text _descriptionText;

        private MysticRewardItemBase _item;

        internal void Init(MysticRewardItemBase item, Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
            _item = item;
            _itemVisual.SetUp(item.Icon(), item.TroopStars(), item.Rarity(), item.IsHero());
            _priceText.text = item.Price().ToString();
            _descriptionText.text = item.Description();
            _confirmButton.interactable = _gems.Value >= _item.Price();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseDialog);
            _confirmButton.onClick.AddListener(ClaimReward);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseDialog);
            _confirmButton.onClick.RemoveListener(ClaimReward);
        }

        private void ClaimReward()
        {
            _gems.Value -= _item.Price();
            _item.ClaimReward();
            OnItemPurchased?.Invoke(_item);
            Destroy(gameObject);
        }

        private void CloseDialog()
        {
            Destroy(gameObject);
        }
    }
}
