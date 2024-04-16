using System;
using UnitsData;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroDialogLevelContainer : MonoBehaviour
    {
        [SerializeField] private Text _levelText;
        [SerializeField] private Text _shardsText;
        [SerializeField] private HeroLevelUpConfigSO _levelUpConfig;
        [SerializeField] private Image _shardsProgress;

        internal void SetData(int level, int shards)
        {
            
            _levelText.text = "Lv. " + level.ToString();
            if (level >= _levelUpConfig.MaxLevel)
            {
                _shardsText.text = "MAX";
            }
            else
            {
                var shardsCost = _levelUpConfig.GetShardsForUpgrade(level);
                _shardsText.text = shards + "/" + shardsCost;
                var shardsProgressScale = _shardsProgress.transform.localScale;
                shardsProgressScale.x = Mathf.Clamp((float)shards / (float)shardsCost, 0, 1);
                _shardsProgress.transform.localScale = shardsProgressScale;
            }
            
        }
    }
}
