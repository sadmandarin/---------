using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class BattleTerrain : MonoBehaviour
    {
        internal Transform BoardPosition => _boardPosition;
        internal Transform EnemiesPosition => _enemiesPosition;
        internal Transform MainCameraPosition => _mainCameraPosition;
        internal Transform BarrackCameraPosition => _barracksCameraPosition;
        internal Transform FlagStartingPosition => _flagStartingPosition;

        internal bool IsApplyOffsetsEnabled { get => _applyOffsets; private set => _applyOffsets = value; }

        [SerializeField] private Transform _boardPosition;
        [SerializeField] private Transform _enemiesPosition;
        [SerializeField] private Transform _mainCameraPosition;
        [SerializeField] private Transform _barracksCameraPosition;
        [SerializeField] private Transform _flagStartingPosition;
        [SerializeField] private bool _applyOffsets = true;
    }
}
