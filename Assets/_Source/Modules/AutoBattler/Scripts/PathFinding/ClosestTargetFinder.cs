using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class ClosestTargetFinder : TargetFinder
    {
        internal override Transform FindTarget(List<Transform> possibleTargets)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            for (int i = 0; i < possibleTargets.Count; i++)
            {
                Vector3 directionToTarget = possibleTargets[i].position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = possibleTargets[i];
                }
            }
            Target = bestTarget;
            return bestTarget;
        }
    }
}
