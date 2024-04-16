using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    public class ShopPageRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private RectTransform _resourcesContent;
        [SerializeField] private float _anchoredPositionOfCoins = 1200f;
        [SerializeField] private float _anchoredPositionOfFreeItems = 2700f;
        [SerializeField] private Button _tab1, _tab2;

        public void OpenResoucesGemsPanel()
        {
            _tab2.onClick.Invoke();
            _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, 0);
            
        }

        public void OpenResoucesCoinsPanel()
        {
            _tab2.onClick.Invoke();
            _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, _anchoredPositionOfCoins);
        }

        public void OpenFreeItems()
        {
            _tab1.onClick.Invoke();
            _resourcesContent.anchoredPosition = new Vector2(_content.anchoredPosition.x, _anchoredPositionOfFreeItems);
        }
    }
}
