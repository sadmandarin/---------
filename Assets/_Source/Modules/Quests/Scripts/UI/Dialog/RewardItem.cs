using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class RewardItem : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Text _quantityText;

        internal void SetUp(int quantity, Sprite icon)
        {
            _iconImage.sprite = icon;
            _quantityText.text = quantity.ToString();
        }
    }
}
