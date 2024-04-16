using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class RandomTargetFinder : TargetFinder
    {
        internal override Transform FindTarget(List<Transform> possibleTargets)
        {
            Target = possibleTargets[Random.Range(0, possibleTargets.Count)];
            return Target;
        }
    }
}
