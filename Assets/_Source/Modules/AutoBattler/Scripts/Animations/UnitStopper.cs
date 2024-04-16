using UnityEngine;
using UnityEngine.AI;

namespace AutoBattler
{
    internal class UnitStopper : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private float _previousSpeed;
        private float _previousAcceleration;

        internal void StopMoving()
        {
            _agent.isStopped = true;
        }

        internal void ResumeMoving()
        {
            _agent.ResetPath();
            _agent.isStopped = false;
        }
    }
}
