using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AutoBattler
{
    [RequireComponent(typeof(NavMeshAgent))]
    internal class PathFinder : MonoBehaviour
    {
        internal bool TargetInReach => _targetInReach;
        internal Action ReachedTarget;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _rotationTime = 5;
        [SerializeField] private float _rangeToAttack;

        private Transform _target;

        private bool _targetInReach;
        private bool _targetSet = false;

        internal void SetMovementSpeed(float speedMovement)
        {
            //_agent.acceleration = speedMovement;
            _agent.speed = speedMovement;
        }

        internal void SetNewTarget(Transform target)
        {
            _target = target;
            if (_agent.isOnNavMesh == false)
            {
                StartCoroutine(UpdateNavMeshAgent(target));
                return;
            }
            _agent.ResetPath();
            _agent.SetDestination(_target.position);
            _targetSet = true;
        }

        private IEnumerator UpdateNavMeshAgent(Transform target)
        {
            _agent.enabled = false;
            yield return new WaitForEndOfFrame();
            _agent.enabled = true;
            _agent.ResetPath();
            _agent.SetDestination(_target.position);
            _targetSet = true;
        }

        internal void ChangeAttackRange(float newRangeToAttack)
        {
            _rangeToAttack = newRangeToAttack;
        }

        private void Update()
        {
            if (_agent.destination == null || _target == null /*|| _agent.destination == transform.position*/)
            {
                _targetInReach = false;
                return;
            }

            if (_targetSet == false)
                return;

            if (_agent.pathPending)
            {
                return;
            }

            _agent.destination = _target.position;
            RotateTowardsTargetOvertime();

            

            if (_targetInReach == false && _agent.remainingDistance <= _rangeToAttack)
            {
                _targetInReach = true;
                ReachedTarget?.Invoke();
            }

            if (_targetInReach && _agent.remainingDistance > _rangeToAttack)
            {
                _targetInReach = false;
            }
        }

        private void RotateTowardsTargetOvertime()
        {
            var targetRotation = Quaternion.LookRotation(_target.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationTime * Time.deltaTime);
        }
    }
}
