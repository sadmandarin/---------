using Lean.Localization;
using PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitsData;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class TroopsChestTip : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private List<UnitPurchaseDataSO> _unitsToBuy;
        [SerializeField] private LevelVariable _mainLevelVariable;
        [SerializeField] private LeanPhrase _normalPhrase, _rarePhrase;
        [SerializeField] private UnitAdder _unitAdder;
        [SerializeField] private TroopChestRewardsConfig _config;

        private void OnEnable()
        {
            _text.text = GetTroopsToBeUnlocked();
        }

        private string GetTroopsToBeUnlocked()
        {
            StringBuilder sb = new StringBuilder();
            var availableTroops = _config.GetAllAvailableUnits();  
            var notCommonTroops = availableTroops.Where(n => n.UnitView.Rarity > 0).ToList();
            var troops = notCommonTroops.Count > 0 ? notCommonTroops : availableTroops;

            if (notCommonTroops.Count == 0)
            {
                sb.AppendLine("<b><color=grey> " + LeanLocalization.GetTranslationText(_normalPhrase.name)
                                          + " 100%</color></b>");
                foreach (UnitPurchaseDataSO unit in availableTroops)
                {
                    string troopName = LeanLocalization.GetTranslationText(unit.UnitView.Name.ToString());
                    if (unit.UnitView.Rarity == 0)
                    {
                        sb.AppendLine(troopName);
                    }
                }
            }
            else
            {
                sb.AppendLine("<color=grey><b>" + LeanLocalization.GetTranslationText(_normalPhrase.name)
                                          + " 40%</b></color>");
                foreach (UnitPurchaseDataSO unit in availableTroops)
                {
                    string troopName = LeanLocalization.GetTranslationText(unit.UnitView.Name.ToString());
                    if (unit.UnitView.Rarity == 0)
                    {
                        sb.AppendLine(troopName);
                    }
                }
                sb.AppendLine();
                sb.AppendLine("<b><color=blue>" + LeanLocalization.GetTranslationText(_rarePhrase.name)
                              + " 60%" + "</color></b>");

                foreach (UnitPurchaseDataSO unit in availableTroops)
                {
                    string troopName = LeanLocalization.GetTranslationText(unit.UnitView.Name.ToString());
                    if (unit.UnitView.Rarity == 1)
                    {
                        sb.AppendLine(troopName);
                    }
                }
            }

            
            return sb.ToString();
        }
    }
}
