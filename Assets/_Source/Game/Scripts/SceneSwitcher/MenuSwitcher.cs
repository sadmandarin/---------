using AutoBattler;
using GridBoard;
using MainPage;
using PersistentData;
using ShopPage;
using System;
using System.Collections;
using System.Collections.Generic;
using TerritoryPage;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class MenuSwitcher : MonoBehaviour
    {
        [SerializeField] private MenuBase _mainMenu;
        [SerializeField] private MenuBase _inBattleMenu;
        [SerializeField] private AutoBattlerAndBoardRoot _battlerRoot;
        [SerializeField] private MainPageTabGroup _mainPageTabGroup;
        [SerializeField] private ShopPageRoot _shopPageRoot;
        [SerializeField] private TerritoryPageRoot _territoryPageRoot;
        [SerializeField] private VoidEventChannelSO _moveToCoins;
        [SerializeField] private VoidEventChannelSO _moveToGems;
        [SerializeField] private VoidEventChannelSO _moveToTerritory;
        [SerializeField] private VoidEventChannelSO _onSwitchedToBattle;
        [SerializeField] private VoidEventChannelSO _onSwitchedToMainMenu;
        [SerializeField] private VoidEventChannelSO _onMoveToBattle;
        [SerializeField] private VoidEventChannelSO _onMoveToMissions;
        [SerializeField] private VoidEventChannelSO _onMoveToFreeItems;
        [SerializeField] private VoidEventChannelSO _onMoveToOfflineRewards;
        [SerializeField] private VoidEventChannelSO _onMoveToChests;
        [SerializeField] private GameObject _experienceEffect;
        [SerializeField] private HeroTopInfo _heroTopInfo;
        [SerializeField] private MainPageTabsOpener _mainPageTabsOpener;
        [SerializeField] private StartBattleButton _startBattleButton;
        [SerializeField] private LevelConfigBaseSO _mainLevelsConfig;

        private bool _alreadyInMainMenu = true;

        private void OnEnable()
        {
            _moveToCoins.OnEventRaised += MoveToCoins;
            _moveToGems.OnEventRaised += MoveToGems;
            _heroTopInfo.OnButtonPressed += MoveToHeroPage;
            _moveToTerritory.OnEventRaised += MoveToTerritory;
            _onMoveToMissions.OnEventRaised += MoveToMissions;
            _onMoveToFreeItems.OnEventRaised += MoveToFreeItems;
            _onMoveToOfflineRewards.OnEventRaised += MoveToOfflineRewards;
            _onMoveToChests.OnEventRaised += MoveToChests;
            _onMoveToBattle.OnEventRaised += MoveToBattle;
        }

        private void OnDisable()
        {
            _moveToCoins.OnEventRaised -= MoveToCoins;
            _moveToGems.OnEventRaised -= MoveToGems;
            _heroTopInfo.OnButtonPressed -= MoveToHeroPage;
            _moveToTerritory.OnEventRaised -= MoveToTerritory;
            _onMoveToMissions.OnEventRaised -= MoveToMissions;
            _onMoveToFreeItems.OnEventRaised -= MoveToFreeItems;
            _onMoveToOfflineRewards.OnEventRaised -= MoveToOfflineRewards;
            _onMoveToChests.OnEventRaised -= MoveToChests;
            _onMoveToBattle.OnEventRaised -= MoveToBattle;
        }

        private void MoveToGems()
        {
            SwitchViewToResoucesPage(false);
        }

        private void MoveToHeroPage()
        {
            SwitchToMainMenu();
            _mainPageTabGroup.SelectTab(1);
        }

        internal void SwitchToBattle(LevelConfigBaseSO levelConfig)
        {
            _mainMenu.Hide();
            _inBattleMenu.Show();
            _battlerRoot.Init(levelConfig);
            _alreadyInMainMenu = false;
            _experienceEffect.SetActive(false);
            _onSwitchedToBattle.RaiseEvent();
        }

        internal void SwitchToMainMenu()
        {
            _mainMenu.Show();
            _inBattleMenu.Hide();
            _battlerRoot.ResetBattle();
            _territoryPageRoot.EnableHeadHuntDialog();
            _alreadyInMainMenu = true;
            _experienceEffect.SetActive(true);
            _onSwitchedToMainMenu.RaiseEvent();
            // Если во время боя включено ускорение и игрок возвращается назад, надо сбросить скорость
            if (Time.timeScale == 2)
                Time.timeScale = 1;
        }

        internal void SwitchViewToResoucesPage(bool moveToCoins)
        {
            if (_alreadyInMainMenu == false)
                SwitchToMainMenu();
            _mainPageTabGroup.SelectTab(0);
            if (moveToCoins)
                _shopPageRoot.OpenResoucesCoinsPanel();
            else
                _shopPageRoot.OpenResoucesGemsPanel();
        }

        private void MoveToCoins()
        {
            SwitchViewToResoucesPage(true);
        }

        private void MoveToTerritory()
        {
            if (_alreadyInMainMenu == false)
                SwitchToMainMenu();
            _mainPageTabGroup.SelectTab(3);
        }

        private void MoveToMissions()
        {
            if (_alreadyInMainMenu == false)
                SwitchToMainMenu();
            _mainPageTabGroup.SelectTab(3);
            _territoryPageRoot.SelectItem(1);
        }

        private void MoveToFreeItems()
        {
            if (_alreadyInMainMenu == false)
                SwitchToMainMenu();
            _mainPageTabGroup.SelectTab(0);
            _shopPageRoot.OpenFreeItems();
        }

        private void MoveToChests()
        {
            if (_alreadyInMainMenu == false)
                SwitchToMainMenu();
            _mainPageTabGroup.SelectTab(2);
            _mainPageTabsOpener.OpenChestsTab();
        }

        private void MoveToOfflineRewards()
        {
            if (_alreadyInMainMenu == false)
                SwitchToMainMenu();
            _mainPageTabGroup.SelectTab(2);
            _mainPageTabsOpener.OpenOfflineRewardsTab();
        }

        private void MoveToBattle()
        {
            if (_alreadyInMainMenu == false)
                SwitchToMainMenu();
            SwitchToBattle(_mainLevelsConfig);
        }
    }
}
