using UnityEngine;

namespace AutoBattler
{
    internal class EnemyAppearanceMaker : MonoBehaviour
    {
        [SerializeField] private Renderer _unitRenderer;
        [SerializeField] private Material _enemyMaterial;

        internal void ChangeAppearanceToEnemy()
        {
            _unitRenderer.material = _enemyMaterial;
        }
    }
}
