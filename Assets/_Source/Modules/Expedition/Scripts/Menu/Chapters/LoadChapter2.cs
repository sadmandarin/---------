using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Expedition
{
    public class LoadChapter2 : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private ConquestLevelsCollection levelsCollection;

        [SerializeField]
        private LevelVariable levelVariable;

        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private ChapterItemRequirementDescription chapterDescription;

        [SerializeField]
        private GameObject castleLock;

        [SerializeField]
        private Image castleImage;

        [SerializeField]
        private RectTransform rectTransform;

        private void Start()
        {
            chapterDescription.SetUp(80, 1);
            UpdateView();
        }

        internal void UpdateView()
        {
            castleImage.color = IsChapter2Unlocked() ? Color.white : new Color(0.67f, 0.67f, 0.67f, 1);
            castleLock.SetActive(IsChapter2Unlocked() == false);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        public void LoadChapterScene()
        {
            if (IsChapter2Unlocked())
            {
                SceneManager.LoadScene(4);
            }

            else
            {
                canvasGroup.alpha = canvasGroup.alpha == 0 ? 1 : 0;
            }
        }

        private bool IsChapter2Unlocked()
        {
            return levelsCollection.GetLevelData(20).Finished && levelVariable.Value >= 80;
        }
    }
}
