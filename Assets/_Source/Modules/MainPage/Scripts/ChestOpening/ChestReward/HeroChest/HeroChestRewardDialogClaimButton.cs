using PersistentData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class HeroChestRewardDialogClaimButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _dialogParent;

        private void OnEnable()
        {
            _button.onClick.AddListener(CloseDialog);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(CloseDialog);
        }

        private void CloseDialog()
        {
            Destroy(_dialogParent);
        }

    }
}
