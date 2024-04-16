using Lean.Localization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    internal class BattleReportRankPageTabGroup : MonoBehaviour
    {
        [SerializeField] private List<BattleReportRankPageTab> _tabs;
        [SerializeField] private BattleReportRankPage _rankPage;
        [SerializeField] private LeanLocalizedText _statText;

        private void Awake()
        {
            foreach (var tab in _tabs)
            {
                tab.TabPressed += ChangeTab;
            }
        }

        internal void ChangeTab(BattleReportStat stat)
        {
            foreach (var tab in _tabs)
            {
                if (tab.Stat == stat) 
                    tab.Select();
                else
                    tab.Unselect();
            }
            _statText.TranslationName = _tabs.First(n => n.Stat == stat).TranslationName;
            _rankPage.ChangeTabs(stat);
        }
    }
}
