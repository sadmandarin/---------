using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AutoBattler
{
    public class MagicApprenticeMover : MonoBehaviour
    {
        [SerializeField] private FurthestTargetFinder _furthestTargetFinder;
        [SerializeField] private PathFinder _pathFinder;
        [SerializeField] private ClosestTargetFinder _closestTargetFinder;
        [SerializeField] private AutoBattlerUnit _unit;
        [SerializeField] private Transform _unitTransform;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _heightOfFlight = 10;

        private bool _alreadyFoundTarget;

        private void Awake()
        {
            _unitTransform.DOLocalMoveY(_heightOfFlight, 0.5f);
            _pathFinder.ReachedTarget += HandleTargetReached;
            _furthestTargetFinder.TargetFound += HandleTargetFound;
        }

        private void HandleTargetReached()
        {
            if (_alreadyFoundTarget)
                return;
            _alreadyFoundTarget = true;
            _unitTransform.DOLocalMoveY(0, 0.5f);
            _agent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        }

        private void HandleTargetFound(Vector3 position)
        {
            _unit.SetNewTargetFinder(_closestTargetFinder);
        }
    }
}
