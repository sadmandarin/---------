using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class UnitQuantityStat : MonoBehaviour
    {
        [SerializeField] private Text _quantityText;

        internal void Set(bool isSingleCount, int level)
        {
            _quantityText.text = GetUnitQuantity(isSingleCount, level).ToString();
        }

        private int GetUnitQuantity(bool isSingleCount, int level)
        {
            if (isSingleCount)
                return 1;

            switch (level)
            {
                case 1: return 3;
                case 2: return 5;
                case 3: return 7;
                default: return 9;
            }

        }
    }
}