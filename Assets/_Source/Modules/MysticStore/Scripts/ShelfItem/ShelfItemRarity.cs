using UnityEngine;
using UnityEngine.UI;

namespace MysticStore
{
    internal class ShelfItemRarity : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Image _heroMark;
        [SerializeField] private Sprite[] _frameRarities;
        [SerializeField] private Sprite[] _heroRarities;

        internal void SetRarity(int rarity, bool isHero)
        {
            _frame.sprite = _frameRarities[rarity];
            if (isHero == false)
                _heroMark.gameObject.SetActive(false);
            else
            {
                _heroMark.gameObject.SetActive(true);
                _heroMark.sprite = _heroRarities[rarity];
            }
        }
    }
}