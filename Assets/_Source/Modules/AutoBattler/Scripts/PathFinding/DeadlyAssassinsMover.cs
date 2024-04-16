using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class DeadlyAssassinsMover : MonoBehaviour
    {
        [SerializeField] private FurthestTargetFinder _furthestTargetFinder;
        [SerializeField] private ClosestTargetFinder _closestTargetFinder;
        [SerializeField] private AutoBattlerUnit _unit;

        private void Start()
        {
            _furthestTargetFinder.TargetFound += TeleportAndChangeTargetFinder;
        }

        private void TeleportAndChangeTargetFinder(Vector3 position)
        {
            StartCoroutine(Teleport(position));
            _unit.SetNewTargetFinder(_closestTargetFinder);
        }

        private IEnumerator Teleport(Vector3 position)
        {
            yield return new WaitForSeconds(0.5f);
            transform.position = position + transform.forward;
        }
    }
}
