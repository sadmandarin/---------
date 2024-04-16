using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    internal class RendererHider : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        internal void ToggleVisibility()
        {
            _renderer.enabled = !_renderer.enabled;
        }

        internal void Hide()
        {
            _renderer.enabled = false;
        }

        internal void Show()
        {
            _renderer.enabled = true;
        }
    }
}
