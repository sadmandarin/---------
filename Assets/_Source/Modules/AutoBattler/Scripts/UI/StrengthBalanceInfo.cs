using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

namespace AutoBattler
{
    internal class StrengthBalanceInfo : MonoBehaviour
    {
        [SerializeField] private Text _allyNumbersText;
        [SerializeField] private Text _enemyNumbersText;
        [SerializeField] private Text _allyAttackText;
        [SerializeField] private Text _enemyAttackText;
        [SerializeField] private Text _allyDefenseText;
        [SerializeField] private Text _enemyDefenseText;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _statsPanel;

        internal void CalculateAndSetStats(List<AutoBattlerUnit> units)
        {
            var playerUnits = units.Where(n => n.Faction == Faction.Player).ToList();
            var enemyUnits = units.Where(n => n.Faction == Faction.Enemy).ToList();

            var allyNumbers = playerUnits.Count;
            var enemyNumbers = enemyUnits.Count;
            float allyAttack = playerUnits.Sum(n => n.AttackStat);
            var enemyAttack = enemyUnits.Sum(n => n.AttackStat);
            var allyDefense = playerUnits.Sum(n => n.DefenseStat);
            var enemyDefense = enemyUnits.Sum(n => n.DefenseStat);

            UpdateStats(allyNumbers, enemyNumbers, allyAttack, allyDefense, enemyAttack, enemyDefense);
        }

        private void UpdateStats(int allyNumbers, int enemyNumbers, float allyAttack, float allyDefense, float enemyAttack, float enemyDefense)
        {
            _allyNumbersText.text = allyNumbers.ToString();
            _allyAttackText.text = allyAttack.ToString();
            _allyDefenseText.text = allyDefense.ToString();
            _enemyNumbersText.text = enemyNumbers.ToString();
            _enemyAttackText.text = enemyAttack.ToString();
            _enemyDefenseText.text = enemyDefense.ToString();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_allyNumbersText.transform.parent.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(_enemyNumbersText.transform.parent.GetComponent<RectTransform>());
        }

        internal void ShowStats()
        {
            _statsPanel.SetActive(true);
        }

        internal void HideStats()
        {
            _statsPanel.SetActive(false);
        }

        internal void Hide()
        {
            _canvasGroup.alpha = 0;
        }

        internal void Show()
        {
            _canvasGroup.alpha = 1;
        }

    }
}
