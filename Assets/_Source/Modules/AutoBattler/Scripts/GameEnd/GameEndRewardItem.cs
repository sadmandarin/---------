using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class GameEndRewardItem : MonoBehaviour
    {
        [SerializeField] private Text _valueText;
        [SerializeField] private Image _icon;

        internal void SetUp(Sprite rewardIcon, int quantityOfReward)
        {
            _icon.sprite = rewardIcon;
            _valueText.text = "x" + quantityOfReward;
        }
    }
}
