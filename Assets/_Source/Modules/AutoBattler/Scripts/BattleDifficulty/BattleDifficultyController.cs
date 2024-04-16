using Lean.Localization;
using PersistentData;
using System;
using System.Collections;
using UnityEngine;

namespace AutoBattler
{
    internal class BattleDifficultyController : MonoBehaviour
    {
        [SerializeField] private BattleDifficultyView _view;
        [SerializeField] private BattleDifficultyConfig _config;
        [SerializeField] private BattleDifficultyVariable _variable;
        [SerializeField] private AutoBattlerRoot _autoBattlerRoot;
        [SerializeField] private AutoBattlerTip _tipPrefab;
        [SerializeField] private Canvas _canvas;

        private AutoBattlerTip _activeTip;

        internal void ChangeDifficulty(BattleDifficulty battleDifficulty)
        {
            _variable.Value = battleDifficulty;
            _autoBattlerRoot.ReInit();
            UpdateView();
        }

        internal void ShowTip(int level)
        {
            if (_activeTip != null)
            {
                Destroy(_activeTip.gameObject);
            }
            var tip = Instantiate(_tipPrefab, _canvas.transform);
            tip.SetUp(level);
            _activeTip = tip;
        }

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var difficultyData = _config.GetDifficultyData(_variable.Value);
            _view.SetView(difficultyData.Skull, difficultyData.BgImage,
                LeanLocalization.GetTranslationText(difficultyData.Difficulty.ToString()));
            _view.HideTip();
        }

        private IEnumerator UpdateBattle()
        {
            //_autoBattlerRoot.ResetBattle();
            yield return new WaitForEndOfFrame();
            _autoBattlerRoot.ReInit();
        }


    }
}
