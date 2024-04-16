using DG.Tweening;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class AutoBattlerTip : MonoBehaviour
    {
        [SerializeField] private LeanPhrase _unlockingRequirement;
        [SerializeField] private Text _text;
        [SerializeField] private float _timeToDissapear;
        [SerializeField] private float _timeToMoveUp;
        [SerializeField] private float _moveUpBy;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _timeToWaitAfterMovingUp;

        internal void SetUp(int levelToUnlock)
        {
            var localizedString = LeanLocalization.GetTranslationText(_unlockingRequirement.name);
            _text.text = string.Format(localizedString, levelToUnlock);
        }

        private void Awake()
        {
            StartDissapearing();
        }

        private void StartDissapearing()
        {
            DOTween.Sequence().Append(transform.DOLocalMoveY(_moveUpBy, _timeToMoveUp))
                              .AppendInterval(_timeToWaitAfterMovingUp)
                              .Append(_canvasGroup.DOFade(0, _timeToDissapear))
                              .AppendCallback(() => Destroy(gameObject));
        }
    }
}
