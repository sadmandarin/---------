using AutoBattler;
using UnityEngine;
using UnityEngine.Pool;

internal class ProjectileExplosion : MonoBehaviour
{
    private ObjectPool<Projectile> _pool;
    private Projectile _projectile;

    internal void Init(ObjectPool<Projectile> pool, Projectile projectile)
    {
        _pool = pool;
        _projectile = projectile;
    }


    private void OnParticleSystemStopped()
    {
        _pool.Release(_projectile);
        gameObject.SetActive(false);
    }
}
