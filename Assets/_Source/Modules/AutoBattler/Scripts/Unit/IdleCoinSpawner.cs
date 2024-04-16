using DG.Tweening;
using UnityEngine;

namespace AutoBattler
{
    internal class IdleCoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _idleCoinPrefab;
        [SerializeField] private AutoBattlerUnitsManager _unitsManager;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _randomOffsetMultiplier = 1;
        [SerializeField] private float _timeAfterLyingDown;
        [SerializeField] private Transform _chestPosition;
        [SerializeField] private Transform _coinsParent;

        internal void Start()
        {
            _unitsManager.EnemyUnitDied += SpawnCoins;
        }

        internal void SpawnCoins(AutoBattlerUnit unitThatDied)
        {
            for (int i = 0; i < 3; i++)
            {
                var randomOffset = Random.insideUnitCircle * _randomOffsetMultiplier;
                var randomPosition = unitThatDied.transform.position
                                              + new Vector3(randomOffset.x, randomOffset.y, 0);
                var spawnedCoin = Instantiate(_idleCoinPrefab, unitThatDied.transform.position, unitThatDied.transform.rotation);
                spawnedCoin.transform.SetParent(_coinsParent);
                DOTween.Sequence().Append(spawnedCoin.transform.DOJump(randomPosition, _jumpPower, 1, _jumpTime))
                                  .AppendInterval(_timeAfterLyingDown)
                                  .Append(spawnedCoin.transform.DOMove(_chestPosition.transform.position, _jumpTime))
                                  .AppendCallback(() => Destroy(spawnedCoin.gameObject));

            }
            
        }

    }
}
