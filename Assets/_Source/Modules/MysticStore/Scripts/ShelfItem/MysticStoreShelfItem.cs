using System;
using UnityEngine;
using UnityEngine.UI;

namespace MysticStore
{
    internal class MysticStoreShelfItem : MonoBehaviour
    {
        internal Action<MysticRewardItemBase> OnPressed;
        internal MysticRewardItemBase RewardItem => _rewardItem;
        [SerializeField] private Text _priceText;
        [SerializeField] private MysticStoreItemVisual _itemVisual;
        [SerializeField] private Button _itemButton;
        [SerializeField] private GameObject _pricePanel, _soldOutPanel;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Color _grayedOutColor;

        private MysticRewardItemBase _rewardItem;

        private void OnEnable()
        {
            _itemButton.onClick.AddListener(PressItem);
        }

        private void OnDisable()
        {
            _itemButton.onClick.RemoveListener(PressItem);
        }

        private void PressItem()
        {
            OnPressed?.Invoke(_rewardItem);
        }

        internal void Init(MysticRewardItemBase rewardItem, bool isSoldOut)
        {
            _rewardItem = rewardItem;
            _itemVisual.SetUp(rewardItem.Icon(), rewardItem.TroopStars(), rewardItem.Rarity(), rewardItem.IsHero());
            _priceText.text = rewardItem.Price().ToString();
            if (isSoldOut)
                MakeSoldOut();
            
        }

        internal void MakeSoldOut()
        {
            _itemButton.enabled = false;
            _iconImage.color = _grayedOutColor;
            _pricePanel.SetActive(false);
            _soldOutPanel.SetActive(true);
        }
    }
}