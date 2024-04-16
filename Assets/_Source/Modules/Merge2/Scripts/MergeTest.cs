using UnityEngine;

namespace Merge2
{
    public class MergeTest : MonoBehaviour
    {
        [SerializeField] private MergableItem _item1;
        [SerializeField] private MergableItem _item2;
        [SerializeField] private MergeRoot _mergeSystem;

        [ContextMenu("Merge")]
        public void Merge()
        {
            IMergableItem newItem;
            _mergeSystem.TryMerge(_item1, _item2, out newItem);
        }
    }
}