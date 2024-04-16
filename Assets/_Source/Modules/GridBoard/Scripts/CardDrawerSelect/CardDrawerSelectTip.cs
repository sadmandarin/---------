using Lean.Localization;
using PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitsData;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class CardDrawerSelectTip : MonoBehaviour
    {
        [SerializeField] private Text _normalArmyText, _rareArmyText; 
        [SerializeField] private Text _luckyArmyText;
        [SerializeField] private LevelVariable _levelVariable;
        [SerializeField] private UnitPurchaseDataList _purchaseDataList;
        [SerializeField] private LeanPhrase _normal, _rare;

        private void OnEnable()
        {
            List<UnitPurchaseDataSO> normalTroops = _purchaseDataList.UnitPurchaseData.Where(n =>
                                                    n.LevelRequirement <= _levelVariable.Value &&
                                                    n.UnitView.Rarity == 0).ToList();

            List<UnitPurchaseDataSO> rareTroops = _purchaseDataList.UnitPurchaseData.Where(n =>
                                                    n.LevelRequirement <= _levelVariable.Value &&
                                                    n.UnitView.Rarity == 1).ToList();

            List<UnitPurchaseDataSO> allRareTroops = _purchaseDataList.UnitPurchaseData.Where(n => n.UnitView.Rarity == 1).ToList();

            StringBuilder stringBuilder = new StringBuilder();
            string normal = LeanLocalization.GetTranslationText(_normal.name);
            stringBuilder.AppendLine("<color=grey>" + normal + "</color>");
            foreach (var normalTroop in normalTroops)
            {
                stringBuilder.AppendLine("<b>" + LeanLocalization.GetTranslationText(normalTroop.UnitView.Name.ToString()) + "</b>");
            }

            _normalArmyText.text = stringBuilder.ToString();

            stringBuilder.Clear();

            string rare = LeanLocalization.GetTranslationText(_rare.name);
            stringBuilder.AppendLine("<color=blue>" + rare + "</color>");
            foreach (var troop in rareTroops)
            {
                stringBuilder.AppendLine("<b>" + LeanLocalization.GetTranslationText(troop.UnitView.Name.ToString()) + "</b>");
            }

            _rareArmyText.text = stringBuilder.ToString();

            stringBuilder.Clear();

            stringBuilder.AppendLine("<color=blue>" + rare + "</color>");
            foreach (var troop in allRareTroops)
            {
                stringBuilder.AppendLine("<b>" + LeanLocalization.GetTranslationText(troop.UnitView.Name.ToString()) + "</b>");
            }

            _luckyArmyText.text = stringBuilder.ToString();
        }

    }
}
