using UnityEngine;
using UnityEngine.UI;

namespace MysticStore
{
    internal class MysticStoreItemVisual : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private ShelfItemStars _shelfItemStars;
        [SerializeField] private ShelfItemRarity _shelfItemRarity;

        internal void SetUp(Sprite itemIcon, int troopStars, int rarity, bool isHero)
        {
            _iconImage.sprite = itemIcon;
            _shelfItemStars.SetStars(troopStars);
            _shelfItemRarity.SetRarity(rarity, isHero);
        }
    }
}