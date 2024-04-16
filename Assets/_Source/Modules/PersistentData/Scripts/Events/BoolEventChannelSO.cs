using System;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Events/Bool")]
    public class BoolEventChannelSO : ScriptableObject
    {
        public Action<bool, GameObject> OnEventRaised;

        public void RaiseEvent(bool value, GameObject objectThatRaisedEvent)
        {
            OnEventRaised?.Invoke(value, objectThatRaisedEvent);
        }
    }
}
