using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    internal class PathFindingTest : MonoBehaviour
    {
        [SerializeField] private PathFinder[] _pathFinder;
        [SerializeField] private TargetFinder[] _targetFinder;
        [SerializeField] private List<Transform> _enemies;

        private void Start()
        {
            for (int i = 0; i < _targetFinder.Length; i++)
            {
                TargetFinder targetFinder = _targetFinder[i];
                targetFinder.FindTarget(_enemies.Select(n => n.transform).ToList());
            }
            for (int i = 0; i < _pathFinder.Length; i++)
            {
                PathFinder pathFinder = _pathFinder[i];
                pathFinder.SetNewTarget(_targetFinder[i].Target);
            }
            
        }
    }
}
