using DragAndDrop;
using Merge2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeToDrag
{
    public class MergeToDragSystem : MonoBehaviour
    {
        [SerializeField] private MergeRoot _mergeSystem;
        [SerializeField] private DragRoot _dragRoot;
        [SerializeField] private float _radius;

        private void OnEnable()
        {
            _dragRoot.Dropped += HandleDropped;
        }

        private void OnDisable()
        {
            _dragRoot.Dropped -= HandleDropped;
        }

        private void HandleDropped(GameObject obj)
        {
            bool foundMergable = true;
            RaycastHit[] hits = new RaycastHit[2];
            Physics.SphereCastNonAlloc(obj.transform.position, _radius, Vector3.up, hits);
            IMergableItem[] mergableItems = new IMergableItem[2];
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider == null || hits[i].transform.TryGetComponent(out IMergableItem mergableItem) == false)
                {
                    foundMergable = false;
                    break;
                }
                else
                {
                    mergableItems[i] = mergableItem;
                }
            }

            IMergableItem nextItem;
            if (foundMergable)
            {
                _mergeSystem.TryMerge(mergableItems[0], mergableItems[1], out nextItem);
            }
            
        }
    }
}
