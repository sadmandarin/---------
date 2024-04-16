using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public class BattleRemainArmy : MonoBehaviour
    {
        [SerializeField] private Text _numberOfRemainingTroopsText;
        [SerializeField] private BattleReportButton _battleReportButton;
        [SerializeField] private Image _troopsIcon;
        [SerializeField] private Sprite _allyTroopSprite;
        [SerializeField] private Sprite _enemyTroopSprite;
        [SerializeField] private Text _alliesSurvivedText;
        [SerializeField] private Text _enemiesSurvivedText;
        internal void Init(int numberOfRemainingTroops, BattleReportRoot reportRoot, bool hasWon)
        {
            _alliesSurvivedText.gameObject.SetActive(hasWon);
            _enemiesSurvivedText.gameObject.SetActive(!hasWon);
            _troopsIcon.sprite = hasWon ? _allyTroopSprite : _enemyTroopSprite;

            _numberOfRemainingTroopsText.text = numberOfRemainingTroops.ToString();
            _battleReportButton.Init(reportRoot, hasWon);
        }
    }
}
