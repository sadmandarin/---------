using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class SingleCardContainer : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Image _troopIcon;
        [SerializeField] private Image _bgFrame;
        [SerializeField] private Image _bgName;
        [SerializeField] private Sprite[] _bgFrameRarities;
        [SerializeField] private Sprite[] _bgNameRarities;
        [SerializeField] private Color[] _nameColorRarities;
        [SerializeField] private Image[] _stars;

        internal void SetUp(string name, Sprite troopIcon, int rarity, int level)
        {
            _nameText.text = name;
            _nameText.color = _nameColorRarities[rarity];
            _troopIcon.sprite = troopIcon;
            _bgFrame.sprite = _bgFrameRarities[rarity];
            _bgName.sprite = _bgNameRarities[rarity];
            for (int i = 0; i < level; i++)
            {
                _stars[i].gameObject.SetActive(true);
            }
        }
    }
}
