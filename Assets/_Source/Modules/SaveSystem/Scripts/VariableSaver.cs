using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class VariableSaver : MonoBehaviour
    {
        public Action OnVariableSaveInitialized;

        [SerializeField] private SaveableVariablesController _variablesController;
        [SerializeField] private SaveSystemRoot _saveSystem;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Init()
        {
            _variablesController.BarracksHolder.UnitsChanged += HandleUnitsChanged;
            _variablesController.BoardHolder.UnitsChanged += HandleUnitsChanged;
            _variablesController.Gems.OnValueChanged += HandleFloatVariableChanged;
            _variablesController.Coins.OnValueChanged += HandleFloatVariableChanged;
            _variablesController.Experience.OnValueChanged += HandleIntVariableChanged;
            _variablesController.MainLevel.OnValueChanged += HandleIntVariableChanged;
            _variablesController.HeroCollection.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.ExperienceRewardCollection.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.CollectedUnitsCollection.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.CardDrawerProgressionIndex.OnValueChanged += HandleIntVariableChanged;
            _variablesController.TroopsChest.ChestQuantityChanged += HandleSaveableDataChanged;
            _variablesController.HeroesChest.ChestQuantityChanged += HandleSaveableDataChanged;
            _variablesController.HeroesChestSavedTime.OnValueChanged += HandleSaveableTimeChanged;
            _variablesController.TroopsChestSavedTime.OnValueChanged += HandleSaveableTimeChanged;
            _variablesController.OfflineIncomeTime.OnValueChanged += HandleSaveableTimeChanged;
            _variablesController.MysticStoreItemsCollection.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.DailyUpdateTime.OnValueChanged += HandleSaveableTimeChanged;
            _variablesController.NormalMissionsLevel.OnValueChanged += HandleIntVariableChanged;
            _variablesController.HardMissionsLevel.OnValueChanged += HandleIntVariableChanged;
            _variablesController.NormalMissionsTimesRemaining.OnValueChanged += HandleIntVariableChanged;
            _variablesController.HardMissionsTimesRemaining.OnValueChanged += HandleIntVariableChanged;
            _variablesController.DailyTroopsCollection.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.ClaimedFreeDaily.OnValueChanged += HandleBoolVariableChanged;
            _variablesController.ConquestLevels.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.ConquestRewards.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.ConquestRewards2.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.PurchasedConquestPremiumRewards.OnValueChanged += HandleBoolVariableChanged;
            _variablesController.PurchasedConquestPremiumRewards2.OnValueChanged += HandleBoolVariableChanged;
            _variablesController.QuestsCollection.CollectionChanged += HandleSaveableDataChanged;
            _variablesController.LuckyBonus.OnValueChanged += HandleIntVariableChanged;
            _variablesController.AutoBattleTimes.OnValueChanged += HandleIntVariableChanged;
            _variablesController.DoubleSpeed.OnValueChanged += HandleBoolVariableChanged;
            _variablesController.BattleDifficulty.OnValueChanged += HandleDifficultyChanged;
            _variablesController.PurchasedAutoFight.OnValueChanged += HandleBoolVariableChanged;
            OnVariableSaveInitialized?.Invoke();
        }

        private void HandleDifficultyChanged(BattleDifficulty difficulty)
        {
            _saveSystem.Save();
        }

        private void OnDestroy()
        {
            _variablesController.BarracksHolder.UnitsChanged -= HandleUnitsChanged;
            _variablesController.BoardHolder.UnitsChanged -= HandleUnitsChanged;
            _variablesController.Gems.OnValueChanged -= HandleFloatVariableChanged;
            _variablesController.Coins.OnValueChanged -= HandleFloatVariableChanged;
            _variablesController.Experience.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.MainLevel.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.HeroCollection.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.ExperienceRewardCollection.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.CollectedUnitsCollection.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.CardDrawerProgressionIndex.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.TroopsChest.ChestQuantityChanged -= HandleSaveableDataChanged;
            _variablesController.HeroesChest.ChestQuantityChanged -= HandleSaveableDataChanged;
            _variablesController.HeroesChestSavedTime.OnValueChanged -= HandleSaveableTimeChanged;
            _variablesController.TroopsChestSavedTime.OnValueChanged -= HandleSaveableTimeChanged;
            _variablesController.OfflineIncomeTime.OnValueChanged -= HandleSaveableTimeChanged;
            _variablesController.MysticStoreItemsCollection.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.DailyUpdateTime.OnValueChanged -= HandleSaveableTimeChanged;
            _variablesController.NormalMissionsLevel.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.HardMissionsLevel.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.NormalMissionsTimesRemaining.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.HardMissionsTimesRemaining.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.DailyTroopsCollection.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.ClaimedFreeDaily.OnValueChanged -= HandleBoolVariableChanged;
            _variablesController.ConquestLevels.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.ConquestRewards.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.ConquestRewards2.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.PurchasedConquestPremiumRewards.OnValueChanged -= HandleBoolVariableChanged;
            _variablesController.PurchasedConquestPremiumRewards2.OnValueChanged -= HandleBoolVariableChanged;
            _variablesController.QuestsCollection.CollectionChanged -= HandleSaveableDataChanged;
            _variablesController.LuckyBonus.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.AutoBattleTimes.OnValueChanged -= HandleIntVariableChanged;
            _variablesController.DoubleSpeed.OnValueChanged -= HandleBoolVariableChanged;
            _variablesController.BattleDifficulty.OnValueChanged -= HandleDifficultyChanged;
            _variablesController.PurchasedAutoFight.OnValueChanged += HandleBoolVariableChanged;
        }

        private void HandleBoolVariableChanged(bool obj)
        {
            _saveSystem.Save();
        }

        private void HandleSaveableTimeChanged(JsonDateTime time)
        {
            _saveSystem.Save();
        }

        private void HandleSaveableDataChanged()
        {
            _saveSystem.Save();
        }

        private void HandleUnitsChanged()
        {
            _saveSystem.Save();
        }

        private void HandleIntVariableChanged(int obj)
        {
            _saveSystem.Save();
        }

        private void HandleFloatVariableChanged(float obj)
        {
            _saveSystem.Save();
        }
    }
}
