using UnityEngine;

namespace MainPage
{
    internal class MainPageObjectsHider : MonoBehaviour
    {
        internal bool IsActive { get; private set; } = true;

        [SerializeField] private GameObject[] _gameObjectsToToggleDuringDialog;
        [SerializeField] private Transform[] _parentsWithRendererHiders;

        internal void ToggleVisibility()
        {
            IsActive = !IsActive;
            ToggleGameObjectDuringDialog();
            ToggleAllRenderers();
        }

        private void ToggleGameObjectDuringDialog()
        {
            foreach (var item in _gameObjectsToToggleDuringDialog)
            {
                item.SetActive(item.activeSelf ? false : true);
            }
        }

        private void ToggleAllRenderers()
        {
            foreach (var parent in _parentsWithRendererHiders)
            {
                foreach (Transform child in parent.transform)
                {
                    if (child.gameObject.TryGetComponent(out RendererHider rendererHider))
                        rendererHider.ToggleVisibility();
                }
            }
        }
    }
}
