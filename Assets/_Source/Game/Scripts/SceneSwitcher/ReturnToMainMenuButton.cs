using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class ReturnToMainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MenuSwitcher _battleSceneSwitcher;

        private void OnEnable()
        {
            _button.onClick.AddListener(_battleSceneSwitcher.SwitchToMainMenu);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_battleSceneSwitcher.SwitchToMainMenu);
        }
    }
}
