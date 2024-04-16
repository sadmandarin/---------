using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class OutlineVisualChanger : VisualChanger
    {
        [SerializeField] private Outline _outline;

        internal override void ToggleVisual(bool isActive)
        {
            _outline.enabled = isActive;
        }
    }
}
