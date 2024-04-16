using UnityEngine;
using PersistentData;

namespace ShopPage
{
    internal class DailyItemsDailyActivity : DailyActivityBase
    {
        [SerializeField] private DailyItemsController _itemsController;
        [SerializeField] private BoolVariableSO _claimedFreeDailyItem;
        public override void InvokeDailyActivity()
        {
            _claimedFreeDailyItem.Value = false;
            _itemsController.UpdateDailyTroops();
            _itemsController.UpdateFreeDaily();
        }
    }

}
