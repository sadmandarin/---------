using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class BattleTerrainInitializer : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _boardTransform;
        [SerializeField] private Transform _enemyParentTransform;
        [SerializeField] private Transform _cameraMainTransform;
        [SerializeField] private Transform _cameraBarracksTransform;
        [SerializeField] private Transform _terrainParent;
        [SerializeField] private Transform _flagStartingTransform;
        [SerializeField] private BattleTerrainOffsetApplier _offsetApplier;
        
        private BattleTerrain _currentTerrain;

        internal void InitTerrain(BattleTerrain terrain)
        {
            if (_currentTerrain != null)
                ClearTerrain();
            _currentTerrain = Instantiate(terrain, _terrainParent);
            if (_currentTerrain.IsApplyOffsetsEnabled == false)
                CopyPositionsFromTerrain();
            else
                ApplyOffsets();
        }

        private void CopyPositionsFromTerrain()
        {
            _mainCamera.transform.SetPositionAndRotation(_currentTerrain.MainCameraPosition.transform.position,
                _currentTerrain.MainCameraPosition.transform.rotation);
            _boardTransform.SetPositionAndRotation(_currentTerrain.BoardPosition.transform.position,
                            _currentTerrain.BoardPosition.transform.rotation);
            _enemyParentTransform.SetPositionAndRotation(_currentTerrain.EnemiesPosition.transform.position,
                _currentTerrain.EnemiesPosition.transform.rotation);
            _cameraMainTransform.SetPositionAndRotation(_currentTerrain.MainCameraPosition.transform.position,
                _currentTerrain.MainCameraPosition.transform.rotation);
            _cameraBarracksTransform.SetPositionAndRotation(_currentTerrain.BarrackCameraPosition.transform.position,
                _currentTerrain.BarrackCameraPosition.transform.rotation);
            if (_currentTerrain.FlagStartingPosition != null)
            {
                _flagStartingTransform.SetPositionAndRotation(_currentTerrain.FlagStartingPosition.transform.position,
                    _currentTerrain.FlagStartingPosition.transform.rotation);
            }
        }

        internal void ApplyOffsets()
        {
            _boardTransform.SetPositionAndRotation(_currentTerrain.BoardPosition.transform.position,
                            _currentTerrain.BoardPosition.transform.rotation);
            _offsetApplier.ApplyBarracksOffset(_cameraBarracksTransform, _boardTransform);
            _offsetApplier.ApplyCameraOffset(_cameraMainTransform, _boardTransform);
            _offsetApplier.ApplyCameraOffset(_mainCamera.transform, _boardTransform);

            if (_currentTerrain.FlagStartingPosition != null)
            {
                _flagStartingTransform.SetPositionAndRotation(_currentTerrain.FlagStartingPosition.transform.position,
                    _currentTerrain.FlagStartingPosition.transform.rotation);
            }
        }

        internal void ClearTerrain()
        {
            if (_currentTerrain != null)
                Destroy(_currentTerrain.gameObject);
        }
    }
}
