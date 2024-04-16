using UnityEngine;

namespace Legion
{
    public class TabContent : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public virtual void Hide()
        {
            _canvasGroup.alpha = 0;
        }

        public virtual void Show()
        {
            if (gameObject.activeInHierarchy == false)
                gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }

        public virtual void HideCamera() { }
        public virtual void ShowCamera() { }
    }
}