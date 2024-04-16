using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class MeteorShowerAnimationHandler : MonoBehaviour
    {
        private Action _action;

        internal void Init(Action action)
        {
            _action = action;
        }

        internal void HandleMeteorReachedTarget()
        {
            _action.Invoke();
        }

        internal void HandleAnimationEnded()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
