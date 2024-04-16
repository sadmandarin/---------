using MainPage;
using Quests;
using System;
using UnityEngine;

namespace Legion
{
    public class QuestsToMainPageRoot : MonoBehaviour
    {
        [SerializeField] private ItemCollectionAnimation _itemCollectionAnimation;
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private QuestsEventsRoot _eventsRoot;
        [SerializeField] private Camera _infiniteBattleCamera;

        private void OnEnable()
        {
            _questsCompleter.OnQuestRewardClaimed += HandleOnQuestRewardClaimed;
            _eventsRoot.OnDialogOpened += HideInfiniteBattle;
            _eventsRoot.OnDialogClosed += ShowInfiniteBattle;
        }

        private void ShowInfiniteBattle()
        {
            _infiniteBattleCamera.gameObject.SetActive(true);
        }

        private void HideInfiniteBattle()
        {
            _infiniteBattleCamera.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _questsCompleter.OnQuestRewardClaimed -= HandleOnQuestRewardClaimed;
            _eventsRoot.OnDialogOpened -= HideInfiniteBattle;
            _eventsRoot.OnDialogClosed -= ShowInfiniteBattle;
        }

        private void HandleOnQuestRewardClaimed()
        {
            if (this != null && gameObject != null)
            {
                if (_itemCollectionAnimation != null)
                    _itemCollectionAnimation.PlayAnimations(true, true);
            }
                
        }
    }
}
