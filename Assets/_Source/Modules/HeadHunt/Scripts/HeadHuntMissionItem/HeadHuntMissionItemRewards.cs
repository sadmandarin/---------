using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeadHunt
{
    internal class HeadHuntMissionItemRewards : MonoBehaviour
    {
        [SerializeField] private Text _coinsText, _gemsText;
        [SerializeField] private GameObject _coinsParent, _gemsParent;
        [SerializeField] private ExtraRewardBase _coinsReward, _gemsReward;

        private LevelVariable _levelVariable;
        private ExtraRewardsBaseConfig _rewardsConfig;

        private void OnEnable()
        {
            if (_levelVariable == null || _rewardsConfig == null)
                return;
            GetRewardsAndUpdateTexts();
        }

        internal void Init(LevelVariable levelVariable, ExtraRewardsBaseConfig rewardsConfig)
        {
            _levelVariable = levelVariable;
            _rewardsConfig = rewardsConfig;
            GetRewardsAndUpdateTexts();
        }

        private void GetRewardsAndUpdateTexts(int level = 0)
        {
            int currentLevel = _levelVariable.Value;
            var extraRewards = _rewardsConfig.GetRewardByLevel(currentLevel);
            int gems = 0, coins = 0;
            foreach (var extraReward in extraRewards)
            {
                if (extraReward == _coinsReward)
                    coins += _rewardsConfig.GetQuantityForReward(extraReward, currentLevel);
                if (extraReward == _gemsReward)
                    gems += _rewardsConfig.GetQuantityForReward(extraReward, currentLevel);
            }
            UpdateTexts(coins, gems);
        }

        internal void UpdateTexts(int coins, int gems)
        {
            if (coins == 0)
            {
                _coinsParent.gameObject.SetActive(false);
            }
            else
            {
                _coinsParent.gameObject.SetActive(true);
            }
                

            if (gems == 0)
            {
                _gemsParent.gameObject.SetActive(false);
            }
            else
            {
                _gemsParent.gameObject.SetActive(true);
            }
                

            _coinsText.text = "x" + coins;
            _gemsText.text = "x" + gems;
        }
    }
}
