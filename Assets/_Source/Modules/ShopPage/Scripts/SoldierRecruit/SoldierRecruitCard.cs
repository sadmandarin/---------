using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    internal class SoldierRecruitCard : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Image _troopIcon;
        [SerializeField] private Image _bgFrame;
        [SerializeField] private Image _bgName;
        [SerializeField] private Sprite[] _bgFrameRarities;
        [SerializeField] private Sprite[] _bgNameRarities;
        [SerializeField] private Image _starPrefab;
        [SerializeField] private Transform _starParent;

        internal void SetUp(string name, Sprite troopIcon, int rarity, int level)
        {
            _nameText.text = name;
            _troopIcon.sprite = troopIcon;
            _bgFrame.sprite = _bgFrameRarities[rarity];
            _bgName.sprite = _bgNameRarities[rarity];
            for (int i = 0; i < level; i++)
            {
                Instantiate(_starPrefab, _starParent);
            }
        }
    }
}
