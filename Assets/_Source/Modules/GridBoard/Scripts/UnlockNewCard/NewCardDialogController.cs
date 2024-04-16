using PersistentData;
using System.Collections.Generic;
using System.Linq;
using UnitsData;
using UnityEngine;

namespace GridBoard
{
    internal class NewCardDialogController : MonoBehaviour
    {
        [SerializeField] private UnlockNewCardDialog _dialogPrefab;
        [SerializeField] private LevelVariable _currentLevel;
        [SerializeField] private List<UnitPurchaseDataSO> _unlockableUnits;
        [SerializeField] private Camera _canvasCamera;

        private void OnEnable()
        {
            if (_unlockableUnits.Any(n => n.LevelRequirement + 1 == _currentLevel.Value))
            {
                var UnitView = _unlockableUnits.First(n => n.LevelRequirement + 1 == _currentLevel.Value);
                var dialog = Instantiate(_dialogPrefab);
                string localizedName = Lean.Localization.LeanLocalization.GetTranslationText(UnitView.UnitView.Name.ToString());
                dialog.Init(_canvasCamera, UnitView.UnitView.Icon, localizedName, UnitView.UnitView.Rarity);
            }
        }
    }
}
