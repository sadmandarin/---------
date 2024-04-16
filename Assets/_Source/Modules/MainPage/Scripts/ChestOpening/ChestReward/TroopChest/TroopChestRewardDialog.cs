using PersistentData;
using System;
using System.Collections.Generic;
using UnitsData;
using UnityEngine;

namespace MainPage
{
    internal class TroopChestRewardDialog : ChestRewardDialog
    {
        [SerializeField] private ChestRewardCardInitializer _initializer;
        [SerializeField] private TroopChestRewardDialogClaimButton _claimButton;
        [SerializeField] private ExperienceAdder _experienceAdder;
        [SerializeField] private TroopChestRewardsConfig _config;

        public override void SetUp()
        {
            int level = 1;
            _config.GetRandomTroopData(out string defaultName, out Sprite troopIcon, out string troopName, out int rarity);
            _initializer.InitCard(troopIcon, troopName, rarity);
            _experienceAdder.AddExperienceForBuyingTroops(rarity, level - 1);
            _claimButton.Init(defaultName, level);
        }
    }
}
