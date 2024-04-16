using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestRewardCardInitializer : MonoBehaviour
    {
        [SerializeField] private Image _troopIcon;
        [SerializeField] private Text _troopName;
        [SerializeField] private Image _bgFrame;
        [SerializeField] private Image _bgName;
        [SerializeField] private Image _cardBack;
        [SerializeField] private Sprite[] _bgFrameRarities;
        [SerializeField] private Sprite[] _bgNameRarities;
        [SerializeField] private Sprite[] _cardBackRarities;
        [SerializeField] private Color[] _nameColorRarities;

        internal void InitCard(Sprite troopIcon, string name, int rarity)
        {
            _troopIcon.sprite = troopIcon;
            _troopName.text = name;
            _troopName.color = _nameColorRarities[rarity];
            _bgFrame.sprite = _bgFrameRarities[rarity];
            _bgName.sprite = _bgNameRarities[rarity];
            _cardBack.sprite = _cardBackRarities[rarity];
        }
    }
}
