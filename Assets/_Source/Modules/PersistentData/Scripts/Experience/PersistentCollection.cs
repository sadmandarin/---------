using System;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    public class PersistentCollection<T> : ScriptableObject
    {
        public Action CollectionChanged;
        [field:SerializeField] public List<T> CollectionValue { get; private set; } = new List<T>();

        public virtual void InitWithStartingData()
        {
            CollectionValue.Clear();
        }

        public void Load(List<T> collection)
        {
            CollectionValue = collection;
        }
    }
}
