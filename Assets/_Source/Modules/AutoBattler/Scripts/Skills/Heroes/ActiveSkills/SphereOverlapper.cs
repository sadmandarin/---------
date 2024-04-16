using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal static class SphereOverlapper
    {
        internal static void FindUnitsInsideSphereAndPerformAction(float range, Vector3 startPosition, Action<AutoBattlerUnit> action)
        {
            var hits = Physics.OverlapSphere(startPosition, range);
            Debug.Log("Modifying stats");
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out AutoBattlerUnit unit))
                {
                     action(unit);
                }
            }
        }
    }
}
