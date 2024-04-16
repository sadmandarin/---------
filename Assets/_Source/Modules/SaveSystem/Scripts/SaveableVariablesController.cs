using PersistentData;
using System.Collections.Generic;
using UnityEngine;
namespace SaveSystem
{
    [CreateAssetMenu(menuName ="SaveSystem/VariablesController")]
    internal class SaveableVariablesController : ScriptableObject
    {
        [field: SerializeField] internal BarracksUnitsPersistentHolder BarracksHolder { get; private set; }
        [field: SerializeField] internal BoardUnitsPersistentHolder BoardHolder { get; private set; }
        [field: SerializeField] internal FloatVariableSO Coins { get; private set; } 
        [field: SerializeField] internal FloatVariableSO Gems { get; private set; } 
        [field: SerializeField] internal LevelVariable MainLevel { get; private set; } 
        [field: SerializeField] internal IntVariableSO Experience { get; private set; } 
        [field: SerializeField] internal HeroCollection HeroCollection { get; private set; }
        [field: SerializeField] internal ExperienceRewardCollection ExperienceRewardCollection { get; private set; }
        [field: SerializeField] internal CollectedUnitsCollection CollectedUnitsCollection { get; private set; }
        [field: SerializeField] internal IntVariableSO CardDrawerProgressionIndex { get; private set; }
        [field: SerializeField] internal ChestVariableSO TroopsChest { get; private set; }
        [field: SerializeField] internal ChestVariableSO HeroesChest { get; private set; }
        [field: SerializeField] internal SavedDateTimeVariableSO TroopsChestSavedTime { get; private set; }
        [field: SerializeField] internal SavedDateTimeVariableSO HeroesChestSavedTime { get; private set; }
        [field: SerializeField] internal SavedDateTimeVariableSO OfflineIncomeTime { get; private set; }
        [field: SerializeField] internal MysticStoreItemsCollection MysticStoreItemsCollection { get; private set; }
        [field: SerializeField] internal SavedDateTimeVariableSO DailyUpdateTime { get; private set; }
        [field: SerializeField] internal LevelVariable NormalMissionsLevel { get; private set; }
        [field: SerializeField] internal LevelVariable HardMissionsLevel { get; private set; }
        [field: SerializeField] internal IntVariableSO NormalMissionsTimesRemaining { get; private set; }
        [field: SerializeField] internal IntVariableSO HardMissionsTimesRemaining { get; private set; }
        [field: SerializeField] internal DailyTroopsCollection DailyTroopsCollection { get; private set; }
        [field: SerializeField] internal BoolVariableSO ClaimedFreeDaily { get; private set; }
        [field: SerializeField] internal ConquestLevelsCollection ConquestLevels { get; private set; }
        [field: SerializeField] internal ConquestRewardsCollection ConquestRewards { get; private set; }
        [field: SerializeField] internal ConquestRewardsCollection ConquestRewards2 { get; private set; }
        [field: SerializeField] internal BoolVariableSO PurchasedConquestPremiumRewards { get; private set; }
        [field: SerializeField] internal BoolVariableSO PurchasedConquestPremiumRewards2 { get; private set; }
        [field: SerializeField] internal CompletedQuestsCollection QuestsCollection { get; private set; }
        [field: SerializeField] internal IntVariableSO LuckyBonus{ get; private set; }
        [field: SerializeField] internal IntVariableSO AutoBattleTimes{ get; private set; }
        [field: SerializeField] internal BoolVariableSO DoubleSpeed{ get; private set; }
        [field: SerializeField] internal BattleDifficultyVariable BattleDifficulty { get; private set; }
        [field: SerializeField] internal BoolVariableSO PurchasedAutoFight { get; private set; }
        [field: SerializeField] internal IntVariableSO ProgressPoints { get; private set; }
        [field: SerializeField] internal DailyProgressCollection DailyProgress { get; private set; }

        public Save CreateAndGetEmptySave()
        {
            BarracksHolder.InitWithStartingData();
            BoardHolder.InitWithStartingData();
            Coins.Value = 2400;
            Gems.Value = 0;
            MainLevel.Value = 1;
            Experience.Value = 0;
            HeroCollection.InitWithStartingData();
            ExperienceRewardCollection.InitWithStartingData();
            CollectedUnitsCollection.InitWithStartingData();
            CardDrawerProgressionIndex.Value = 0;
            HeroesChest.ResetData();
            TroopsChest.ResetData();
            TroopsChestSavedTime.ResetData();
            HeroesChestSavedTime.ResetData();
            OfflineIncomeTime.ResetData();
            MysticStoreItemsCollection.InitWithStartingData();
            DailyUpdateTime.Value = (JsonDateTime)DateTimeHelper.GetNextDay7AM();
            NormalMissionsLevel.Value = 1;
            NormalMissionsTimesRemaining.Value = 2;
            HardMissionsLevel.Value = 1;
            HardMissionsTimesRemaining.Value = 2;
            DailyTroopsCollection.InitWithStartingData();
            ClaimedFreeDaily.Value = false;
            ConquestLevels.InitWithStartingData();
            ConquestRewards.InitWithStartingData();
            ConquestRewards2.InitWithStartingData();
            PurchasedConquestPremiumRewards.Value = false;
            PurchasedConquestPremiumRewards2.Value = false;
            QuestsCollection.InitWithStartingData();
            LuckyBonus.Value = 0;
            AutoBattleTimes.Value = 10;
            DoubleSpeed.Value = false;
            BattleDifficulty.Value = (BattleDifficulty)0;
            PurchasedAutoFight.Value = false;
            ProgressPoints.Value = 0;
            DailyProgress.InitWithStartingData();
            return GetCurrentSave();
        }

        public Save GetCurrentSave()
        {
            return new Save(BarracksHolder.Units, BoardHolder.Units, Coins.Value, Gems.Value, MainLevel.Value, 
                Experience.Value, HeroCollection.CollectionValue,
                ExperienceRewardCollection.CollectionValue, CollectedUnitsCollection.CollectionValue, CardDrawerProgressionIndex.Value,
                TroopsChest.QuantityOfChests,HeroesChest.QuantityOfChests, TroopsChest.NumberOfChestsOpened, HeroesChest.NumberOfChestsOpened
                , TroopsChestSavedTime.Value, HeroesChestSavedTime.Value, OfflineIncomeTime.Value,
                MysticStoreItemsCollection.CollectionValue, DailyUpdateTime.Value,
                NormalMissionsLevel.Value, NormalMissionsTimesRemaining.Value,
                HardMissionsLevel.Value, HardMissionsTimesRemaining.Value, DailyTroopsCollection.CollectionValue, ClaimedFreeDaily.Value,
                ConquestLevels.CollectionValue, ConquestRewards.CollectionValue, PurchasedConquestPremiumRewards.Value,
                QuestsCollection.CollectionValue,
                LuckyBonus.Value, ConquestRewards2.CollectionValue, PurchasedConquestPremiumRewards2.Value, AutoBattleTimes.Value,
                DoubleSpeed.Value, BattleDifficulty.Value, PurchasedAutoFight.Value,
                ProgressPoints.Value, DailyProgress.CollectionValue);
        }

        public void LoadSaveIntoData(Save save)
        {
            BarracksHolder.LoadUnits(save.BarracksUnitDatas);
            BoardHolder.LoadUnits(save.BoardUnitDatas);
            Coins.Value = save.Coins;
            Gems.Value = save.Gems;
            MainLevel.Value = save.MainLevel;
            Experience.Value = save.PlayerExperience;
            HeroCollection.Load(save.HeroDatas);
            ExperienceRewardCollection.Load(save.ExperienceRewardDatas);
            CollectedUnitsCollection.Load(save.CollectedUnitDatas);
            HeroesChest.Load(save.QuantityOfHeroChests, save.NumberOfHeroChestsOpened);
            TroopsChest.Load(save.QuantityOfTroopsChests, save.NumberOfTroopsChestsOpened);
            CardDrawerProgressionIndex.Value = save.CardDrawerProgressionIndex;
            TroopsChestSavedTime.Value = save.TroopsChestTime;
            HeroesChestSavedTime.Value = save.HeroChestTime;
            OfflineIncomeTime.Value = save.OfflineIncomeTime;
            MysticStoreItemsCollection.Load(save.MysticStoreItemDatas);
            DailyUpdateTime.Value = save.DailyUpdateTime;
            NormalMissionsLevel.Value = save.NormalMissionsLevel;
            HardMissionsLevel.Value = save.HardMissionsLevel;
            NormalMissionsTimesRemaining.Value = save.NormalMissionsTimesRemaining;
            HardMissionsTimesRemaining.Value = save.HardMissionsTimesRemaining;
            DailyTroopsCollection.Load(save.DailyTroopsData);
            ClaimedFreeDaily.Value = save.ClaimedFreeDaily;
            ConquestLevels.Load(save.ConquestLevelsData);
            ConquestRewards.Load(save.ConquestRewardsData);
            ConquestRewards2.Load(save.ConquestRewardsData2);
            PurchasedConquestPremiumRewards.Value = save.PurchasedConquestPremiumRewards;
            PurchasedConquestPremiumRewards2.Value = save.PurchasedConquestPremiumRewards2;
            QuestsCollection.Load(save.QuestsData);
            AutoBattleTimes.Value = save.AutoBattleTimes;
            LuckyBonus.Value = save.LuckyBonus;
            DoubleSpeed.Value = save.DoubleSpeed;
            BattleDifficulty.Value = save.BattleDifficulty;
            PurchasedAutoFight.Value = save.PurchasedAutoFight;
            ProgressPoints.Value = save.ProgressPoints;
            DailyProgress.Load(save.DailyProgress);
        }
    }
}
