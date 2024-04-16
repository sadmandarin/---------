using AutoBattler;
using HeadHunt;
using System.Collections;
using System.Collections.Generic;
using TerritoryPage;
using UnityEngine;
using PersistentData;
using System.Linq;

namespace Legion
{
    internal class SceneSwitcherRoot : MonoBehaviour
    {
        [SerializeField] private GameEndCanvas _gameEndCanvas;
        [SerializeField] private MenuSwitcher _sceneSwitcher;
        [SerializeField] private AutoBattlerAndBoardRoot _battlerRoot;
        [SerializeField] private AutoBattlerRoot _autoBattlerRoot;
        [SerializeField] private TerritoryPageRoot _territory;
        [SerializeField] private List<LevelConfigBaseSO> _missionsConfigs;


        private void OnEnable()
        {
            _gameEndCanvas.OnEndScreenDismissed += HandleEndScreenDismissed;
            _territory.OnMissionSelected += PlayMission;
        }

        private void OnDisable()
        {
            _gameEndCanvas.OnEndScreenDismissed -= HandleEndScreenDismissed;
            _territory.OnMissionSelected -= PlayMission;
        }

        private void HandleEndScreenDismissed()
        {
            _sceneSwitcher.SwitchToMainMenu();
            _territory.EnableHeadHuntDialog();
        }

        private void PlayMission(LevelVariable variable)
        {
            var levelConfig = _missionsConfigs.FirstOrDefault(n => n.LevelVariable == variable);
            if (levelConfig != null)
            {
                _sceneSwitcher.SwitchToBattle(levelConfig);
            }
        }
    }
}
