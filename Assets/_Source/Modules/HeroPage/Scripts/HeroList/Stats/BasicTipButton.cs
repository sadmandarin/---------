using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class BasicTipButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BasicDescriptionBubble _description;

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleDescription);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleDescription);
        }

        private void ToggleDescription()
        {
            if (_description.IsActive)
                _description.Hide();
            else
                _description.Show();
        }
    }
}
