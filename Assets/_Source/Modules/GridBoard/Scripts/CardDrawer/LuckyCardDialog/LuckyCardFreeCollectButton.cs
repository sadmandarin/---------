using System;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class LuckyCardFreeCollectButton : MonoBehaviour
    {
        internal Action<int> OnFreeCardCollected;

        [SerializeField] private int _cardIndex;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(RaiseEvent);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RaiseEvent);
        }

        private void RaiseEvent()
        {
            OnFreeCardCollected?.Invoke(_cardIndex);
        }
    }
}
