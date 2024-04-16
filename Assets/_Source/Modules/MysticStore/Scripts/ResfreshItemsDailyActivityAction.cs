using PersistentData;
using UnityEngine;

namespace MysticStore
{
    internal class ResfreshItemsDailyActivityAction : DailyActivityActionBase
    {
        [SerializeField] private MysticStoreItemCollectionUpdater _itemUpdater;
        public override void InvokeDailyActivityAction()
        {
            _itemUpdater.RefreshItems();
        }
    }
}
