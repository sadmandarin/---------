using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class ImageSpriteVisualChanger : VisualChanger
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        internal override void ToggleVisual(bool isActive)
        {
            _image.sprite = isActive ? _activeSprite : _inactiveSprite;
        }
    }
}
