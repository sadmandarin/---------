using PersistentData;
using System;
using UnitsData;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroDialogUpgradeLevelButton : MonoBehaviour
    {
        internal Action HeroUpgraded;

        [SerializeField] private Button _button;
        [SerializeField] private Text _upgradeCostText;
        [SerializeField] private HeroLevelUpConfigSO _levelUpConfig;
        [SerializeField] private FloatVariableSO _coins;
        [SerializeField] private HeroCollection _heroCollection;
        [SerializeField] private HeroOriginDialogSpawner _originSpawner;
        [SerializeField] private Sprite _activeBg, _inactiveBg;
        
        private string _heroName;
        private int _shardsForUpgrade;
        private int _shardsThatHeroHas;
        private int _upgradeCost;
        private bool _upgradeAvailable;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleOnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnClick);
        }

        internal void Init(int level, string heroName, int shardThatHeroHas)
        {
            if (level >= _levelUpConfig.MaxLevel)
            {
                gameObject.SetActive(false);
                return;
            }
            _shardsThatHeroHas = shardThatHeroHas;
            _upgradeCost = _levelUpConfig.GetMoneyForUpgrade(level);
            _shardsForUpgrade = _levelUpConfig.GetShardsForUpgrade(level);
            _heroName = heroName;

            _upgradeCostText.text = _upgradeCost.ToString();
            _upgradeAvailable = CanBeUpgraded(_upgradeCost, _coins.Value, _shardsThatHeroHas, _shardsForUpgrade);
            _button.image.sprite = _upgradeAvailable ? _activeBg : _inactiveBg;
        }

        internal void HandleOnClick()
        {
            if (_upgradeAvailable)
            {
                UpdgradeHero();
            }
            else
            {
                SpawnOriginDialog();
            }
            
        }

        private void SpawnOriginDialog()
        {
            _originSpawner.ShowDialog();
        }

        private void UpdgradeHero()
        {
            _heroCollection.UpgradeHero(_heroName, _shardsForUpgrade);
            _coins.Value -= _upgradeCost;
            HeroUpgraded?.Invoke();
        }

        private bool CanBeUpgraded(float cost, float money, int availableShards, int shardsCost)
        {
            return money >= cost && availableShards >= shardsCost;
        }
    }
}
