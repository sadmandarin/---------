using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    public class MainPageTabsOpener : MonoBehaviour
    {
        [SerializeField] private ChestButton _chestButton;
        [SerializeField] private ChestPresser _offlineRewards;

        public void OpenOfflineRewardsTab()
        {
            _offlineRewards.RaiseChestPressed();
        }

        public void OpenChestsTab()
        {
            _chestButton.HandleOnClick();
        }
    }
}
