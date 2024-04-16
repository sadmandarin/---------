using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Merge2
{
    internal class MergableItem : MonoBehaviour, IMergableItem
    {
        [SerializeField, Min(1)] private int _tier = 1;
        [SerializeField] private MergableItem _nextTierItem;

        public int Tier => _tier;

        public bool HasNextTier => _nextTierItem != null;

        public GameObject GetItem =>  gameObject;

        public bool Compare(IMergableItem other)
        {
            return _nextTierItem.Tier == other.Tier;
        }

        public IMergableItem CreateNextTierInstance()
        {
            return Instantiate(_nextTierItem);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }

    public interface IMergableItem
    {
        public int Tier { get; }
        public bool HasNextTier { get; }
        public bool Compare(IMergableItem other);
        public IMergableItem CreateNextTierInstance();
        public void Destroy();
        public GameObject GetItem { get; }
    }
}