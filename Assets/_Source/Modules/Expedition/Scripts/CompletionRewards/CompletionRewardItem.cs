using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class CompletionRewardItem : MonoBehaviour
    {
        [SerializeField] private Text _quantityText;
        [SerializeField] private Image _iconImage;

        internal void SetUp(Sprite icon, int quantity)
        {
            _quantityText.text = "X" + quantity;
            _iconImage.sprite = icon;
        }
    }
}
