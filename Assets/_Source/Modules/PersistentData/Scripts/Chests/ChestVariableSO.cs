using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Chests/Chest")]
    public class ChestVariableSO : ScriptableObject
    {
        public Action ChestQuantityChanged;

        public string Name => LeanLocalization.GetTranslationText(_chestTitle.name);

        [field: SerializeField] public int QuantityOfChests { get; private set; }
        [field: SerializeField] public int[] PredefinedCooldownsInSeconds { get; private set; } 
        [field: SerializeField] public int NumberOfChestsOpened { get; private set; }
        [field: SerializeField] public Sprite ChestIcon { get; private set; }
        [field: SerializeField] public bool UsesGemsForOpening { get; private set; }
        [field: SerializeField] public int AmountOfCurrencyPerInterval { get; private set; }
        [field: SerializeField] public int IntervalTimeInSeconds { get; private set; }
        [field: SerializeField] public SavedDateTimeVariableSO  SavedTime { get; private set; }
        [field: SerializeField] public string ChestSaveName { get; private set; }
        [field: SerializeField] public GameObject ChestDialogPrefab { get; private set; }
        
        [SerializeField] private LeanPhrase _chestTitle;

        public void ResetData()
        {
            NumberOfChestsOpened = 0;
            QuantityOfChests = 0;
        }

        public void Load(int quantity, int timesOpened)
        {
            QuantityOfChests = quantity;
            NumberOfChestsOpened = timesOpened;
        }

        public int GetFullTimeToOpen()
        {
            return PredefinedCooldownsInSeconds[Mathf.Clamp(NumberOfChestsOpened, 0, PredefinedCooldownsInSeconds.Length - 1)];
        }

        public int GetCostToOpenImmediately(int timeLeft)
        {
            int wholeIntervalsLeft = Mathf.Clamp(timeLeft / IntervalTimeInSeconds, 1, int.MaxValue);
            return wholeIntervalsLeft * AmountOfCurrencyPerInterval;
        }

        public void OpenChest()
        {
            QuantityOfChests -= 1;
            NumberOfChestsOpened += 1;
            

            if (QuantityOfChests >= 0)
                ResetChestTimer();

            ChestQuantityChanged?.Invoke();
        }

        public void AddChest()
        {
            if (QuantityOfChests == 0)
                ResetChestTimer();

            QuantityOfChests += 1;
            ChestQuantityChanged?.Invoke();
        }

        private void ResetChestTimer()
        {
            SavedTime.Value = (JsonDateTime)DateTime.Now;
            //PlayerPrefs.SetString(ChestSaveName, JsonUtility.ToJson((JsonDateTime)DateTime.UtcNow));
        }

    }

    [Serializable]
    public struct JsonDateTime
    {
        public long value;
        public static implicit operator DateTime(JsonDateTime jdt)
        {
            //Debug.Log("Converted to time");
            return DateTime.FromFileTimeUtc(jdt.value);
        }
        public static implicit operator JsonDateTime(DateTime dt)
        {
            //Debug.Log("Converted to JDT");
            JsonDateTime jdt = new JsonDateTime();
            jdt.value = dt.ToFileTimeUtc();
            return jdt;
        }
    }
}
