using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestRewardsList : MonoBehaviour
    {
        [SerializeField] private ConquestRewardItem _itemPrefab;
        [SerializeField] private ConquestRewardLevel _levelPrefab;
        [SerializeField] private ConquestRewardsConfig _config;
        [SerializeField] private ConquestLevelsCollection _levelsCollection;
        [SerializeField] private ConquestRewardsCollection _rewardsCollected;
        [SerializeField] private int _startingFromLevel, _toLevel;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private Text _progressText;
        [SerializeField] private int _maxStars = 60;
        [SerializeField] private BoolVariableSO _purchasedBattlePass;
        [SerializeField] private ConquestRewardsCollector _rewardCollector;
        [SerializeField] private ScrollRect _scrollRect;

        private List<GameObject> _contentObjects = new List<GameObject>();
        private List<ConquestRewardItem> _rewardItems = new List<ConquestRewardItem>();
        private ConquestRewardLevel _lastLevel = null;

        [ContextMenu(nameof(UpdateItems))]
        internal void UpdateItems()
        {
            ClearContent();
            InstantiateContent();
            StartCoroutine(FocusOnItem());
            _rewardCollector.Init(_rewardItems);
        }

        private void Start()
        {
            UpdateItems();
        }

        private void InstantiateContent()
        {
            for (int i = 0; i < _config.ConquestRewards.Count; i++)
            {
                var reward = _config.ConquestRewards[i];
                var level = Instantiate(_levelPrefab, _contentParent);
                var starsCollected = _levelsCollection.StarsCollected(_startingFromLevel, _toLevel);
                var isLocked = starsCollected < reward.StarsToGet;
                if (_rewardsCollected.CollectionValue.Count == 0)
                    _rewardsCollected.SetUpStartingData(_config.ConquestRewards.Count);
                var collectedRewards = _rewardsCollected.GetRewardData(i);
                var normalItem = Instantiate(_itemPrefab);
                normalItem.SetUp(_rewardsCollected, reward.NormalReward, reward.NormalQuantity, isLocked, collectedRewards.NormalRewardCollected, false, i);
                var vipItem = Instantiate(_itemPrefab);
                bool vipLocked = isLocked || _purchasedBattlePass.Value == false;
                vipItem.SetUp(_rewardsCollected, reward.VipReward, reward.VipQuantity, vipLocked, collectedRewards.VipRewardCollected, true, i);
                level.SetUp(normalItem, vipItem, reward.StarsToGet, starsCollected);
                _progressText.text = starsCollected + "/" + _maxStars;
                _contentObjects.Add(level.gameObject);
                
                _rewardItems.Add(normalItem);
                _rewardItems.Add(vipItem);

                if (i == 0)
                    _lastLevel = level;

                if (starsCollected > reward.StarsToGet)
                    _lastLevel = level;
            }

            
        }

        private void ClearContent()
        {
            foreach (var contentObject in _contentObjects)
            {
                Destroy(contentObject);
            }

            _contentObjects.Clear();
            _rewardItems.Clear();
        }

        private IEnumerator FocusOnItem()
        {
            yield return new WaitForEndOfFrame();
            ScrollViewFocusFunctions.FocusOnItem(_scrollRect, _lastLevel.RectTransform);
        }
    }
}
