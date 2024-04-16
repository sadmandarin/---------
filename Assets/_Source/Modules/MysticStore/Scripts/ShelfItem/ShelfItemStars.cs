using UnityEngine;

namespace MysticStore
{
    internal class ShelfItemStars : MonoBehaviour
    {
        [SerializeField] private GameObject _lowLevelStarsParent;
        [SerializeField] private GameObject[] _lowLevelStars;

        internal void SetStars(int stars)
        {
            if (stars == 0)
                _lowLevelStarsParent.SetActive(false);
            else
            {
                _lowLevelStarsParent.SetActive(true);
                for (int i = 0; i < stars; i++)
                {
                    _lowLevelStars[i].SetActive(true);
                }
            }
        }
    }
}