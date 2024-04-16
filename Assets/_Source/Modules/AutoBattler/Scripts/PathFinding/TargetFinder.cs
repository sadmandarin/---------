using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal abstract class TargetFinder : MonoBehaviour
    {
        public Transform Target { get; protected set; }

        internal abstract Transform FindTarget(List<Transform> possibleTargets);
    }
}
