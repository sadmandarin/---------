using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeadHunt
{
    public class HeadHuntDialog : MonoBehaviour
    {
        public Action<LevelVariable> OnMissionSelected;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _closeButton;
        [SerializeField] private HeadHuntMissionItem _missionItemPrefab;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private List<HeadHuntMissionSO> _missions;
        [SerializeField] private List<HeadHuntMissionItem> _missionItems;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private VoidEventChannelSO _onMoveToBattle;
        [SerializeField] private VoidEventChannelSO _onMoveToMainMenu;

        public Action OnDialogClosed { get; set; }

        internal void Init(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }

        private void OnEnable()
        {
            foreach (var item in _missionItems)
            {
                Destroy(item.gameObject);
            }

            _missionItems.Clear();
            foreach(var mission in _missions)
            {
                var missionItem = Instantiate(_missionItemPrefab, _contentParent);
                missionItem.Init(mission.Icon, mission.Title, mission.Description, mission.Level, mission.ExtraRewardsConfig, 
                    mission.TimesRemainingVariable, mission.IsHard);
                missionItem.OnMissionButtonClicked += HandleMissionItemClicked;
                _missionItems.Add(missionItem);
            }

            _onMoveToMainMenu.OnEventRaised += EnableHeadHunt;
            _onMoveToBattle.OnEventRaised += DisableHeadHunt;
        }

        private void OnDisable()
        {
            OnDialogClosed?.Invoke();

            _onMoveToMainMenu.OnEventRaised -= EnableHeadHunt;
            _onMoveToBattle.OnEventRaised -= DisableHeadHunt;
        }

        private void HandleMissionItemClicked(LevelVariable levelVariable)
        {
            OnMissionSelected?.Invoke(levelVariable);
        }

        public void EnableHeadHunt()
        {
            _graphicRaycaster.enabled = true;
        }

        internal void DisableHeadHunt()
        {
            _graphicRaycaster.enabled = false;
        }
    }
}
