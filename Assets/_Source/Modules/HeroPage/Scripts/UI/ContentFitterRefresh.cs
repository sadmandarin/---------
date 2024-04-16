using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    public class ContentFitterRefresh : MonoBehaviour
    {
        private void Start()
        {
            RefreshContentFitters();
        }

        [ContextMenu(nameof(RefreshContentFitters))]
        public void RefreshContentFitters()
        {
            var rectTransform = (RectTransform)transform;
            RefreshContentFitter(rectTransform);
        }

        private void RefreshContentFitter(RectTransform rectTransform)
        {
            if (rectTransform == null || !rectTransform.gameObject.activeSelf)
            {
                return;
            }

            foreach (Transform child in rectTransform)
            {
                if (child.gameObject.TryGetComponent(out RectTransform rect))
                    RefreshContentFitter(rect);
            }

            var layoutGroup = rectTransform.GetComponent<LayoutGroup>();
            var contentSizeFitter = rectTransform.GetComponent<ContentSizeFitter>();
            if (layoutGroup != null)
            {
                layoutGroup.SetLayoutHorizontal();
                layoutGroup.SetLayoutVertical();
            }

            if (contentSizeFitter != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            }
        }
    }
}
