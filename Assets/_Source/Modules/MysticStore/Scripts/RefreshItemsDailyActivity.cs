using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MysticStore
{
    internal class RefreshItemsDailyActivity : DailyActivityBase
    {
        [SerializeField] private MysticStoreItemUpdater _itemUpdater;

        public override void InvokeDailyActivity()
        {
            _itemUpdater.UpdateItems();
        }
    }
}
