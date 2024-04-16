using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace AutoBattler
{
    internal class RangedAttacker : Attacker
    {
        internal Action<Vector3> ProjectileLanded;
        
        [SerializeField] private Projectile _projectile;
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private bool _magicDamage;

        private ObjectPool<Projectile> _projectilePool;

        internal override void Attack(AutoBattlerUnit target)
        {
            _unitAnimator.AnimateAttack(() => LaunchProjectileAfterAnimation(target));
        }

        private void LaunchProjectileAfterAnimation(AutoBattlerUnit target)
        {
            if (target == null)
                return;
            var projectile = _projectilePool.Get();
            DOTween.Sequence().Append(projectile.MoveTowardsTarget(target.transform.position))
                              .AppendCallback(projectile.PlayProjectileEffectOnImpact)
                              .AppendCallback(projectile.ResetProjectile)
                              .AppendCallback(() => ProjectileLanded?.Invoke(target.transform.position))
                              .AppendCallback(() => DealDamage(target));
        }

        private void DealDamage(AutoBattlerUnit target)
        {
            if (HitRadius == 0)
            {
                if (target != null)
                {
                    if (_magicDamage)
                        target.TakeMagicHit(BattleReportID, Damage);
                    else
                        target.TakePhysicalHit(BattleReportID, Damage);
                }
            }
            else
            {
                int layerMask = target.gameObject.layer;
                var hits = Physics.OverlapSphere(target.transform.position, HitRadius);
                foreach ( var hit in hits )
                {
                    if (hit.TryGetComponent(out IDamageable unitToHit))
                    {
                        if (_magicDamage)
                            unitToHit.TakeMagicHit(BattleReportID, Damage);
                        else
                            unitToHit.TakePhysicalHit(BattleReportID, Damage);
                    }
                }
            }
        }

        private void Awake()
        {
            _projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGet, OnReturnedToPool, OnDestroyPoolObject);
        }

        private Projectile CreateProjectile()
        {
            var projectile = Instantiate(_projectile, transform);
            projectile.Init(_projectilePool);
            return projectile;
        }

        private void OnGet(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}
