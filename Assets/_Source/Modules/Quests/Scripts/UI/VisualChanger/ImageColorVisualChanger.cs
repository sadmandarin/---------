using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class ImageColorVisualChanger : VisualChanger
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _activeColor = Color.white;
        [SerializeField] private Color _inactiveColor = new Color(255,255,255, 130);
        internal override void ToggleVisual(bool isActive)
        {
            _image.color = isActive ? _activeColor : _inactiveColor;
        }
    }
}
