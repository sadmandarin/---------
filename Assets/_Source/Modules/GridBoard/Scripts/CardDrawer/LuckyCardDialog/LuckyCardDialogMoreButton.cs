using System;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class LuckyCardDialogMoreButton : MonoBehaviour
    {
        internal Action OnMoreLuckyDialogPressed;

        [SerializeField] private Button _button;
        [SerializeField] private Text _chestQuantity;
        [SerializeField] private LuckyConfigSO _config;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleOnClicked);
            UpdateView();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnClicked);
        }

        private void HandleOnClicked()
        {
            OnMoreLuckyDialogPressed?.Invoke();
        }

        private void UpdateView()
        {
            _chestQuantity.text = "x" + _config.GetFullChestQuantity();
            _button.interactable = _config.GetFullChestQuantity() > 0;
        }
    }
}
