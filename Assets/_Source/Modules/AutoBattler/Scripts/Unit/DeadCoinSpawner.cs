using UnityEngine;

namespace AutoBattler
{
    internal class DeadCoinSpawner : MonoBehaviour
    {
        [SerializeField] private DeadCoin _deadCoin;
        [SerializeField] private AutoBattlerUnitsManager _unitsManager;

        internal void Start()
        {
            _unitsManager.EnemyUnitDied += SpawnCoin;
        }

        internal void SpawnCoin(AutoBattlerUnit unitThatDied)
        {
            var deadCoin = Instantiate(_deadCoin, unitThatDied.transform.position, _deadCoin.transform.rotation);
            deadCoin.Init(unitThatDied.RewardForKilling);
        }
    }
}
