using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class TextVisualChanger : VisualChanger
    {
        [SerializeField] private Text _text;
        [SerializeField] private Color _activeColor = Color.white;
        [SerializeField] private Color _inactiveColor = new Color(1,1,1, 0.5f);

        internal override void ToggleVisual(bool isActive)
        {
            _text.color = isActive ? _activeColor : _inactiveColor;
        }
    }
}
