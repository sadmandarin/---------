using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using PersistentData;

namespace SaveSystem
{
    [Serializable]
    public class Save
    {
        public List<BarracksUnitData> BarracksUnitDatas;
        public List<BoardUnitData> BoardUnitDatas;
        public float Coins;
        public float Gems;
        public int MainLevel;
        public int PlayerExperience;
        public List<HeroData> HeroDatas;
        public List<ExperienceRewardData> ExperienceRewardDatas;
        public List<CollectedUnitData> CollectedUnitDatas;
        public int CardDrawerProgressionIndex;
        public int QuantityOfTroopsChests;
        public int QuantityOfHeroChests;
        public int NumberOfTroopsChestsOpened;
        public int NumberOfHeroChestsOpened;
        public JsonDateTime TroopsChestTime;
        public JsonDateTime HeroChestTime;
        public JsonDateTime OfflineIncomeTime;
        public List<MysticStoreItemData> MysticStoreItemDatas;
        public JsonDateTime DailyUpdateTime;
        public int NormalMissionsLevel;
        public int NormalMissionsTimesRemaining;
        public int HardMissionsLevel;
        public int HardMissionsTimesRemaining;
        public List<DailyTroopData> DailyTroopsData;
        public bool ClaimedFreeDaily;
        public List<ConquestLevelsData> ConquestLevelsData;
        public List<ConquestCollectedRewardData> ConquestRewardsData;
        public List<ConquestCollectedRewardData> ConquestRewardsData2;
        public bool PurchasedConquestPremiumRewards;
        public bool PurchasedConquestPremiumRewards2;
        public List<QuestData> QuestsData;
        public int LuckyBonus;
        public int AutoBattleTimes;
        public bool DoubleSpeed;
        public BattleDifficulty BattleDifficulty;
        public bool PurchasedAutoFight;
        public int ProgressPoints;
        public List<DailyProgressCollectedData> DailyProgress;

        public Save(List<BarracksUnitData> barracksUnitDatas, List<BoardUnitData> boardUnitDatas,
            float coins, float gems, int mainLevel, int playerExperience, List<HeroData> heroDatas,
            List<ExperienceRewardData> experienceRewardDatas, List<CollectedUnitData> collectedUnitDatas,
            int cardDrawerProgression, int quantityOfTroopsChests, int quantityOfHeroChests,
            int numberOfTroopsChestsOpened, int numberOfHeroChestsOpened,
            JsonDateTime troopsChestTime, JsonDateTime heroChestTime, JsonDateTime offlineIncomeTime,
            List<MysticStoreItemData> mysticStoreItemDatas, JsonDateTime dailyUpdateTime,
            int normalMissionsLevel, int normalMissionsTimesRemaining,
            int hardMissionsLevel, int hardMissionsTimesRemaining, List<DailyTroopData> dailyTroopsData, bool claimedFreeDaily,
            List<ConquestLevelsData> conquestLevelsData, List<ConquestCollectedRewardData> conquestRewardsData,
            bool purchasedConquestRewards, List<QuestData> questsData, int luckyBonus, List<ConquestCollectedRewardData> conquestRewardsData2,
            bool purchasedConquestPremiumRewards2, int autoBattleTimes, bool doubleSpeed, 
            BattleDifficulty battleDifficulty, bool purchasedAutoFight, int progressPoints, 
            List<DailyProgressCollectedData> dailyProgress)
        {
            BarracksUnitDatas = barracksUnitDatas;
            BoardUnitDatas = boardUnitDatas;
            Coins = coins;
            Gems = gems;
            MainLevel = mainLevel;
            PlayerExperience = playerExperience;
            HeroDatas = heroDatas;
            ExperienceRewardDatas = experienceRewardDatas;
            CollectedUnitDatas = collectedUnitDatas;
            CardDrawerProgressionIndex = cardDrawerProgression;
            QuantityOfTroopsChests = quantityOfTroopsChests;
            QuantityOfHeroChests = quantityOfHeroChests;
            NumberOfTroopsChestsOpened = numberOfTroopsChestsOpened;
            NumberOfHeroChestsOpened = numberOfHeroChestsOpened;
            TroopsChestTime = troopsChestTime;
            HeroChestTime = heroChestTime;
            OfflineIncomeTime = offlineIncomeTime;
            MysticStoreItemDatas = mysticStoreItemDatas;
            DailyUpdateTime = dailyUpdateTime;
            NormalMissionsLevel = normalMissionsLevel;
            NormalMissionsTimesRemaining = normalMissionsTimesRemaining;
            HardMissionsLevel = hardMissionsLevel;
            HardMissionsTimesRemaining = hardMissionsTimesRemaining;
            DailyTroopsData = dailyTroopsData;
            ClaimedFreeDaily = claimedFreeDaily;
            ConquestLevelsData = conquestLevelsData;
            ConquestRewardsData = conquestRewardsData;
            PurchasedConquestPremiumRewards = purchasedConquestRewards;
            QuestsData = questsData;
            LuckyBonus = luckyBonus;
            ConquestRewardsData2 = conquestRewardsData2;
            PurchasedConquestPremiumRewards2 = purchasedConquestPremiumRewards2;
            AutoBattleTimes = autoBattleTimes;
            DoubleSpeed = doubleSpeed;
            BattleDifficulty = battleDifficulty;
            PurchasedAutoFight = purchasedAutoFight;
            ProgressPoints = progressPoints;
            DailyProgress = dailyProgress;
        }
    }
}
