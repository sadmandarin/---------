using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestItemView : MonoBehaviour
    {
        [SerializeField] private GameObject _noBox;
        [SerializeField] private Animator _boxAnimator;
        [SerializeField] private Button[] _chestNotReadyButtons;
        [SerializeField] private Button _chestIsReadyButton;
        [SerializeField] private GameObject _timerParent;

        private const string Jump = "001";
        private const string Idle = "005";

        internal void SetChestView(ChestState state)
        {
            switch (state)
            {
                case ChestState.None:
                    _boxAnimator.Play(Idle);
                    _noBox.SetActive(true);
                    ToggleChestButtons(false);
                    _timerParent.SetActive(false);
                    break;
                case ChestState.NotReady:
                    _boxAnimator.Play(Idle);
                    _noBox.SetActive(false);
                    ToggleChestButtons(false);
                    _timerParent.SetActive(true);
                    break;
                case ChestState.Ready:
                    _boxAnimator.Play(Jump);
                    _noBox.SetActive(false);
                    ToggleChestButtons(true);
                    _timerParent.SetActive(false);
                    break;
            }
        }

        private void ToggleChestButtons(bool isChestReady)
        {
            foreach (var button in _chestNotReadyButtons)
            {
                button.gameObject.SetActive(isChestReady == false);
            }
            _chestIsReadyButton.gameObject.SetActive(isChestReady);
        }
    }
    internal enum ChestState
    {
        None,
        NotReady,
        Ready
    }
}