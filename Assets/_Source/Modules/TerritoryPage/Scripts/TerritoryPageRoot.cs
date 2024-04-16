using HeadHunt;
using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerritoryPage
{
    public class TerritoryPageRoot : MonoBehaviour
    {
        public Action<LevelVariable> OnMissionSelected;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<TerritoryPageItem> _items;
        [SerializeField] private TerritoryTip _tip;
        [SerializeField] private GameObject _infiniteBattleCamera;

        private TerritoryTip _activeTip;
        private bool _isMissionsShowing;
        private HeadHuntDialog _headHuntDialog;

        public void SelectItem(int indexOfItem)
        {
            _items[indexOfItem].HandleOnClick();
        }


        public void EnableHeadHuntDialog()
        {
            if (_headHuntDialog == null)
                return;

            _headHuntDialog.EnableHeadHunt();
        }

        private void LateUpdate()
        {
            if (_isMissionsShowing)
            {
                if (_infiniteBattleCamera!= null && _infiniteBattleCamera.activeInHierarchy)
                    _infiniteBattleCamera.SetActive(false);
            }
        }

        private void OnEnable()
        {
            foreach (var item in _items)
            {
                item.OnDialogSpawned += HandleDialogSpawned;
                item.OnLockedItemClicked += HandleLockedItemClicked;
            }
        }

        private void OnDisable()
        {
            foreach (var item in _items)
            {
                item.OnDialogSpawned -= HandleDialogSpawned;
                item.OnLockedItemClicked -= HandleLockedItemClicked;
            }
        }

        private void HandleDialogSpawned(GameObject dialog)
        {
            var canvas = dialog.GetComponent<Canvas>();
            canvas.worldCamera = _canvas.worldCamera;
            dialog.transform.SetParent(_canvas.transform);
            canvas.overrideSorting = true;
            if (dialog.TryGetComponent(out HeadHuntDialog headHuntDialog))
            {
                _isMissionsShowing = true;
                headHuntDialog.OnMissionSelected += HandleMissionSelected;
                headHuntDialog.OnDialogClosed += () => _isMissionsShowing = false;
                _headHuntDialog = headHuntDialog;
            }
        }

        private void HandleLockedItemClicked(int level)
        {
            if(_activeTip != null)
            {
                Destroy(_activeTip.gameObject);
            }
            var tip = Instantiate(_tip, _canvas.transform);
            tip.SetUp(level);
            _activeTip = tip;
        }


        private void HandleMissionSelected(LevelVariable variable)
        {
            OnMissionSelected?.Invoke(variable);
        }
    }
}
