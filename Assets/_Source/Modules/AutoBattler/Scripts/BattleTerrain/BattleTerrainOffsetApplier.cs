using PersistentData;
using UnityEngine;

namespace AutoBattler
{
    internal class BattleTerrainOffsetApplier : MonoBehaviour
    {
        [SerializeField] private Vector3 _barracksPositionOffset, _barracksRotationOffset;
        [SerializeField] private Vector3 _mainCameraPositionOffset, _mainCameraRotationOffset;
        [SerializeField] private Vector3 _mainCameraPositionSmallOffset, _mainCameraRotationSmallOffset;
        [SerializeField] private LevelVariable _mainLevel;

        internal void ApplyBarracksOffset(Transform barracksTransform, Transform boardTransform)
        {
            barracksTransform.position = boardTransform.position + _barracksPositionOffset;
            barracksTransform.eulerAngles = boardTransform.eulerAngles + _barracksRotationOffset;
        }

        internal void ApplyCameraOffset(Transform cameraTransform, Transform boardTransform)
        {
            if (_mainLevel.Value <= 20)
            {
                cameraTransform.position = boardTransform.position + _mainCameraPositionSmallOffset;
                cameraTransform.eulerAngles = boardTransform.eulerAngles + _mainCameraRotationSmallOffset;
            }
            else 
            {
                cameraTransform.position = boardTransform.position + _mainCameraPositionOffset;
                cameraTransform.eulerAngles = boardTransform.eulerAngles + _mainCameraRotationOffset;
            }
            
        }
    }
}
