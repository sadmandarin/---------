using System;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Events/Int")]
    public class IntEventChannelSO : ScriptableObject
    {
        public Action<int> OnEventRaised;

        public void RaiseEvent(int value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}
