using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class FurthestTargetFinder : TargetFinder
    {
        internal Action<Vector3> TargetFound;
        internal override Transform FindTarget(List<Transform> possibleTargets)
        {
            Transform bestTarget = null;
            float furthestDistanceSqr = 0;
            Vector3 currentPosition = transform.position;
            for (int i = 0; i < possibleTargets.Count; i++)
            {
                Vector3 directionToTarget = possibleTargets[i].position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget > furthestDistanceSqr)
                {
                    furthestDistanceSqr = dSqrToTarget;
                    bestTarget = possibleTargets[i];
                }
            }
            Target = bestTarget;
            TargetFound?.Invoke(bestTarget.position);
            return bestTarget;
        }
    }
}
