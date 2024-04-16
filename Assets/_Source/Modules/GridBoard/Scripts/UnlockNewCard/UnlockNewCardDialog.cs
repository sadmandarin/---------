using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class UnlockNewCardDialog : MonoBehaviour
    {
        [SerializeField] private SingleCardContainer _cardContainer;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _closeButton;

        internal void Init(Camera canvasCamera, Sprite icon, string name, int rarity)
        {
            _canvas.worldCamera = canvasCamera;
            _cardContainer.SetUp(name, icon, rarity, 0);
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(DestroyDialog);
            Invoke("DestroyDialog", 5f);
        }

        private void DestroyDialog()
        {
            if (gameObject == null)
                return;

            Destroy(gameObject);
        }
    }
}
