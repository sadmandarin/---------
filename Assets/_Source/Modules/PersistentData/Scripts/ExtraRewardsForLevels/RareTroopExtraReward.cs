using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "ExtraReward/RareTroop")]
    public class RareTroopExtraReward : ExtraRewardBase
    {
        [SerializeField] private UnitAdder _unitAdder;
        [SerializeField] private UnitViewsListSO _units;
        [SerializeField] private ExperienceAdder _experienceAdder;

        private string _rareTroopName;

        public override void ClaimReward(int quantity)
        {
            if (_rareTroopName == null)
                UpdateTroopName();

            _unitAdder.AddUnit(_rareTroopName.ToString(), 1);
            _experienceAdder.AddExperienceForBuyingTroops(1, 0);
            UpdateTroopName();
        }

        public override void GetRewardDescripton(out Sprite icon, out string description)
        {
            if (_rareTroopName == null)
                UpdateTroopName();

            UnitViewSO unitView = _units.UnitViews.First(n => n.Name.ToString() == _rareTroopName);

            icon = unitView.Icon;
            description = Lean.Localization.LeanLocalization.GetTranslationText(unitView.Name.ToString());
        }

        private void UpdateTroopName()
        {
            List<UnitViewSO> rareTroops = _units.UnitViews.Where(n => n.Rarity == 1).ToList();
            UnitViewSO rareTroop = rareTroops[Random.Range(0, rareTroops.Count)];
            _rareTroopName = rareTroop.Name.ToString();
        }
    }
}
