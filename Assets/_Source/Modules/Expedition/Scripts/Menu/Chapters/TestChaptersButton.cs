using PersistentData;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class TestChaptersButton : MonoBehaviour
    {
        [SerializeField] private LevelVariable _levelVariable;
        [SerializeField] private ConquestLevelsCollection _levelsCollection;
        [SerializeField] private ChapterMenuButton _menu;
        [SerializeField] private Button _button;
        [SerializeField] private LoadChapter2 _chapter2;

        private void OnEnable()
        {
            _button.onClick.AddListener(UnlockChapter2);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(UnlockChapter2);
        }

        private void UnlockChapter2()
        {
            _levelVariable.Value = 81;
            _levelsCollection.FinishLevel(20, 3);
            StartCoroutine(UpdateViewCoroutine());
            if (_chapter2 != null)
                _chapter2.UpdateView();
        }

        private IEnumerator UpdateViewCoroutine()
        {
            _menu.ToggleView();
            yield return new WaitForEndOfFrame();
            _menu.ToggleView();
        }
    }
}
