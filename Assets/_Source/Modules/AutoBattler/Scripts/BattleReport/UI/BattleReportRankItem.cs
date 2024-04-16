using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class BattleReportRankItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _statText;
        [SerializeField] private Image _statBar;
        [SerializeField] private Transform _troopStatsParent;
        [SerializeField] private Text _troopLevel;
        [SerializeField] private Text _troopCount;
        [SerializeField] private Transform _heroStatsParent;
        [SerializeField] private Text _heroLevel;

        internal void SetCommonData(Sprite troopIcon, float statValue, float statNormalized)
        {
            _icon.sprite = troopIcon;
            _statText.text = statValue.ToString("0.");
            var newScale = _statBar.transform.localScale;
            newScale.x = statNormalized;
            _statBar.transform.localScale = newScale;
        }

        internal void SetTroopData(int troopLevel, int troopCount)
        {
            _heroStatsParent.gameObject.SetActive(false);
            _troopStatsParent.gameObject.SetActive(true);
            _troopLevel.text = (troopLevel + 1).ToString();
            _troopCount.text = "x" + troopCount.ToString();
        }

        internal void SetHeroData(int heroLevel)
        {
            _heroStatsParent.gameObject.SetActive(true);
            _troopStatsParent.gameObject.SetActive(false);
            _heroLevel.text = (heroLevel +1).ToString();
        }
    }
}
