using UnityEngine;
using PersistentData;

namespace ShopPage
{
    internal class DailyItemsDailyActivityAction : DailyActivityActionBase
    {
        [SerializeField] private DailyItemsController _itemsController;
        [SerializeField] private BoolVariableSO _claimedFreeDailyItem;

        public override void InvokeDailyActivityAction()
        {
            _claimedFreeDailyItem.Value = false;
            _itemsController.UpdateDailyTroops();
            _itemsController.UpdateFreeDaily();
        }
    }

}
