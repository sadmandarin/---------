using PersistentData;
using UnityEngine;

namespace Legion
{
    internal class DialogThatRaisesEvent : MonoBehaviour
    {
        [SerializeField] private BoolEventChannelSO _boolEvent;

        private void OnEnable()
        {
            _boolEvent.RaiseEvent(true, gameObject);
        }

        private void OnDisable()
        {
            _boolEvent.RaiseEvent(false, gameObject);
        }
    }
}
