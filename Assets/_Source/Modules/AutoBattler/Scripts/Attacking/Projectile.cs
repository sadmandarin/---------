using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace AutoBattler
{
    internal class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private ProjectileExplosion _explosion;

        private Vector3 _targetPosition;
        private ObjectPool<Projectile> _pool;

        internal void Init(ObjectPool<Projectile> pool)
        {
            _pool = pool;
            if (_explosion != null) _explosion.Init(_pool, this);
        }

        internal Tween MoveTowardsTarget(Vector3 position)
        {
            _projectile.gameObject.SetActive(true);
            _targetPosition = position;
            //_projectile.transform.localPosition = Vector3.zero;
            return _projectile.transform.DOJump(position, 0.5f, 1, 0.5f);
        }
        
        internal void ResetProjectile()
        {
            _projectile.gameObject.SetActive(false);
            _projectile.transform.localPosition = Vector3.zero;

            if (_explosion == null )
            { 
                StartCoroutine(ReturnToPool());
            }
            
        }

        private IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(0.5f);
            _pool.Release(this);
        }

        internal void PlayProjectileEffectOnImpact()
        {
            if (_explosion == null)
                return;
            _explosion.gameObject.SetActive(true);
            _explosion.transform.position = _targetPosition;
        }
    }
}
