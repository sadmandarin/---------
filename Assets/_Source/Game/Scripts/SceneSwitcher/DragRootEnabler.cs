using DragAndDrop;
using PersistentData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Legion
{
    internal class DragRootEnabler : MonoBehaviour
    {
        [SerializeField] private DragRoot _dragRoot;
        [SerializeField] private BoolEventChannelSO _boolEvent;

        private List<GameObject> _activeDialogs = new List<GameObject>();

        private void OnEnable()
        {
            _boolEvent.OnEventRaised += ToggleDragRoot;
        }

        private void OnDisable()
        {
            _boolEvent.OnEventRaised -= ToggleDragRoot;
        }

        private void ToggleDragRoot(bool isDialogActive, GameObject objectThatRaisedEvent)
        {
            if (isDialogActive == false)
            {
                if (objectThatRaisedEvent != null && _activeDialogs.Contains(objectThatRaisedEvent))
                    _activeDialogs.Remove(objectThatRaisedEvent);
                if (_activeDialogs.Count == 0)
                    _dragRoot.Activate();
            }
            else
            {
                _dragRoot.Deactivate();
                _activeDialogs.Add(objectThatRaisedEvent);
            }
                
        }
    }
}
