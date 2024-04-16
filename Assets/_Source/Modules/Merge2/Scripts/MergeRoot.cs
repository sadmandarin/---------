using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Merge2
{
    public class MergeRoot : MonoBehaviour
    {
        public Action Merged;

        public bool TryMerge(IMergableItem item1, IMergableItem item2, out IMergableItem nextTierItem)
        {
            if (item1.HasNextTier && item1.Tier == item2.Tier)
            {
                nextTierItem = Merge(item1, item2);
                Merged?.Invoke();
                return true;
            }
            else
            {
                nextTierItem = null;
                return false;
            }
        }

        private IMergableItem Merge(IMergableItem item1, IMergableItem item2)
        {
            IMergableItem nextTierItem = item1.CreateNextTierInstance();

            item1.Destroy();
            item2.Destroy();
            
            return nextTierItem;
        }
    }
}