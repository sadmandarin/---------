using Lean.Localization;
using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ChapterItemRequirementDescription : MonoBehaviour
    {
        [SerializeField] private LeanPhrase _levelRequirement, _chapterRequirement;
        [SerializeField] private Text _levelText, _chapterText;
        [SerializeField] private GameObject _levelCleared, _chapterCleared;
        [SerializeField] private ConquestLevelsCollection _levelsCollection;
        [SerializeField] private LevelVariable _mainLevelVariable;

        internal void SetUp(int levelRequirement, int chapterRequirement)
        {
            var levelText = LeanLocalization.GetTranslationText(_levelRequirement.name);
            _levelText.text = string.Format(levelText, levelRequirement);

            var chapterText = LeanLocalization.GetTranslationText(_chapterRequirement.name);
            _chapterText.text = string.Format(chapterText, chapterRequirement);

            _levelCleared.SetActive(levelRequirement <= _mainLevelVariable.Value);
            _chapterCleared.SetActive(_levelsCollection.HasFinishedLevel(chapterRequirement * 20));


        }
    }
}
