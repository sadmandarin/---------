using PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridBoard
{
    [CreateAssetMenu(menuName = "CardDrawer/LuckyConfig")]
    internal class LuckyConfigSO : ScriptableObject
    {
        [field: SerializeField] public int LuckyBonusRequirement { get; private set; }
        [field: SerializeField] public List<LuckyMultiplierData> LuckyMultipliers { get; private set; }

        [SerializeField] private IntVariableSO _luckyBonus;

        internal bool IsProgressFull => _luckyBonus.Value >= LuckyBonusRequirement;

        internal int GetFullChestQuantity()
        {
            return _luckyBonus.Value / LuckyBonusRequirement;
        }

        internal int GetLuckyMultiplier()
        {
            float randomNumber = Random.Range(0f, 1f);
            var multiplier = LuckyMultipliers.Where(n => 1 - n.Probability <= randomNumber).Max(n => n.Multiplier);
            return multiplier;
        }

        internal void AddLuckyBonus(int multiplier, int level = 1)
        {
            int baseAmount = 4;
            _luckyBonus.Value += multiplier * level * baseAmount;
        }

        internal void ReduceLuckyBonus()
        {
            _luckyBonus.Value -= LuckyBonusRequirement;
        }

        internal int GetPriceForRarity(int rarity)
        {
            return 300;
        }
    }
}
