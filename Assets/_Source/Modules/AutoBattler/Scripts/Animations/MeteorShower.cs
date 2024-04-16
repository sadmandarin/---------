using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class MeteorShower : MonoBehaviour
    {
        [SerializeField] private MeteorShowerAnimationHandler _animationHandler;

        internal void Init(Action action)
        {
            _animationHandler.Init(action);
        }
    }
}
