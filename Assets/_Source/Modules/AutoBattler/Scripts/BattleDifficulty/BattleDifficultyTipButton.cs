using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class BattleDifficultyTipButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BattleDifficultyView _view;

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleTip);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleTip);
        }
        private void ToggleTip()
        {
            _view.ToggleTip();
        }
    }
}
