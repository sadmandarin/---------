using PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    internal class LevelList : MonoBehaviour
    {
        [SerializeField] private List<LevelListItem> _levelListItems = new List<LevelListItem>();
        [SerializeField] private LevelVariable _currentMainLevel;
        [SerializeField] private ExtraRewardsBaseConfig _extraRewardsConfig;

        private void OnEnable()
        {
            UpdateItems();
            _currentMainLevel.OnValueChanged += UpdateItems;
        }

        private void OnDisable()
        {
            _currentMainLevel.OnValueChanged -= UpdateItems;
        }

        private void UpdateItems(int mainLevel = 0)
        {
            for (int i = 0; i < _levelListItems.Count; i++)
            {
                LevelListItem levelItem = _levelListItems[i];
                int levelIndexWithout10 = _currentMainLevel.Value % 10;
                if (levelIndexWithout10 == 0) levelIndexWithout10 = 10;
                var levelIndex = levelIndexWithout10 - 1;
                LevelListItemState state = levelIndex == i ? LevelListItemState.Current
                                                                   : levelIndex > i ?
                                                                   LevelListItemState.Finished : LevelListItemState.Locked;
                var trueLevelIndex = GetTrueLevelFromIndex(i);
                levelItem.SetUp(state, trueLevelIndex);
                var extraRewardForThisLevel = _extraRewardsConfig.GetRewardByLevel(trueLevelIndex);
                var hasExtraReward = extraRewardForThisLevel[0] != null;
                if (hasExtraReward)
                    levelItem.AddReward(extraRewardForThisLevel[0].Icon);
            }
        }

        // Transform i in the for cycle from 1,2,3 to 11,12,13 or 21,22,23
        private int GetTrueLevelFromIndex(int index)
        {
            if (_currentMainLevel.Value % 10 == 0)
            {
                return (_currentMainLevel.Value / 10 - 1) * 10 + index + 1;
            }
            else
            {
                return (_currentMainLevel.Value / 10) * 10 + index + 1;
            }
        }
    }
}
