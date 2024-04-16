using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public class BattleReportButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BattleReportDialog _battleReportDialog;
        [SerializeField] private VoidEventChannelSO _stopAutoFight;

        internal void Init(BattleReportRoot reportRoot, bool hasWon)
        {
            _button.onClick.AddListener(() => InstantiateAndInitializeBattleReport(reportRoot, hasWon));
        }

        private void InstantiateAndInitializeBattleReport(BattleReportRoot reportRoot, bool hasWon)
        {
            var battleReport = Instantiate(_battleReportDialog);
            battleReport.Init(reportRoot, hasWon);
            _stopAutoFight.RaiseEvent();
        }
    }
}
