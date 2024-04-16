using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class DamageRangeStat : MonoBehaviour
    {
        [SerializeField] private Text _numberText;

        internal void Set(float range)
        {
            _numberText.text = range.ToString();
        }
    }
}