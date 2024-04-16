using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestRewardItem : MonoBehaviour
    {
        internal Action<ExtraRewardBase, int> OnRewardClaimed;

        [SerializeField] private Text _quantityText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _borderImage;
        [SerializeField] private Image _maskborderImage;
        [SerializeField] private Sprite _normalBorder;
        [SerializeField] private Sprite _vipBorder;
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _claimed;
        [SerializeField] private GameObject _cover;
        [SerializeField] private Button _claimNormalButton;
        [SerializeField] private Button _claimVipButton;
        [SerializeField] private GameObject _effect;
        [SerializeField] private Text _descriptionText;

        private ConquestRewardsCollection _rewardsCollection;
        private int _quantity, _levelOfReward;
        private ExtraRewardBase _rewardBase;
        private bool _isVip;

        internal void SetUp(ConquestRewardsCollection rewardCollection, ExtraRewardBase extraReward, int quantity, bool isLocked, bool isClaimed, bool isVip, int levelOfReward)
        {
            _rewardsCollection = rewardCollection;
            _quantity = quantity;
            _rewardBase = extraReward;
            _isVip = isVip;
            _levelOfReward = levelOfReward;

            _iconImage.sprite = extraReward.Icon;
            _quantityText.text = "x" + quantity.ToString();
            _borderImage.sprite = isVip ? _vipBorder : _normalBorder;
            _maskborderImage.sprite = isVip ? _vipBorder : _normalBorder;
            _descriptionText.text = extraReward.DescriptionWithQuantity(quantity);


            SetUpView(isLocked, isClaimed, isVip);
        }

        private void SetUpView(bool isLocked, bool isClaimed, bool isVip)
        {
            _cover.SetActive(isLocked);
            _claimNormalButton.gameObject.SetActive(isLocked == false && isClaimed == false && isVip == false);
            _claimVipButton.gameObject.SetActive(isLocked == false && isClaimed == false && isVip);
            _claimed.SetActive(isClaimed);
            _lock.SetActive(isLocked);
        }

        private void OnEnable()
        {
            _claimNormalButton.onClick.AddListener(ClaimReward);
            _claimVipButton.onClick.AddListener(ClaimReward);
        }

        private void OnDisable()
        {
            _claimNormalButton.onClick.RemoveListener(ClaimReward);
            _claimVipButton.onClick.RemoveListener(ClaimReward);
        }

        private void ClaimReward()
        {
            _rewardBase.ClaimReward(_quantity);
            _rewardsCollection.CollectReward(_levelOfReward, !_isVip);
            OnRewardClaimed?.Invoke(_rewardBase, _quantity);
            SetUpView(false, true, _isVip);
        }
    }
}
