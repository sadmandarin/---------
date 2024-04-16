using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoBattler
{
    internal class TroopSpawner : MonoBehaviour
    {
        [SerializeField] private AutoBattlerUnit _unitToSpawn;
        [SerializeField] private AutoBattlerUnitsManager _unitsManager;
        [SerializeField] private Transform _playerUnitsParent;
        [SerializeField] private Camera _sceneCamera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _flagPrefab;
        [SerializeField] private TroopSpawnerCooldownUi _coolDownUi;
        [SerializeField] private Transform _flagPosition;
        [SerializeField] private BattleReportRoot _battleReportRoot;

        private GameObject _spawnedFlag;
        private bool _activated;

        private void OnEnable()
        {
            _coolDownUi.OnButtonPressed += SpawnUnitByFlag;
        }

        private void OnDisable()
        {
            _coolDownUi.OnButtonPressed -= SpawnUnitByFlag;
        }

        internal void Init(bool canSpawnTroops)
        {
            if (canSpawnTroops == false)
                return;

            _coolDownUi.Init();
            _coolDownUi.OnCooldownEnded -= SpawnUnitByFlag;
            _coolDownUi.OnCooldownEnded += SpawnUnitByFlag;
            SpawnFlag(_flagPosition.position);
        }

        private void SpawnUnitByFlag()
        {
            if (_spawnedFlag == null)
                return;
            SpawnUnit(_spawnedFlag.transform.position);
        }

        internal void Activate()
        {
            _activated = true;
        }

        internal void Deactivate()
        {
            _activated = false;
            _coolDownUi.Stop();
        }

        internal void ResetTroopSpawner()
        {
            if(_spawnedFlag != null)
            {
                Destroy(_spawnedFlag);
            }
            _activated = false;
            _coolDownUi.Hide();
            _coolDownUi.Stop();
        }

        private void Update()
        {
            if (_activated == false)
                return;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = GetRayUnderMouse();
                if (Physics.Raycast(ray, out RaycastHit hit, 1000000f, _layerMask))
                {
                    if (hit.transform.TryGetComponent(out ColliderToSpawnTroop colliderToSpawnTroop))
                    {
                        if (_coolDownUi.Quantity > 0)
                            SpawnUnit(hit.point);
                        
                        SpawnFlag(hit.point);
                    }
                }
            }
        }

        private void SpawnUnit(Vector3 position)
        {
            if (_coolDownUi.Quantity <= 0)
                return;
            _coolDownUi.ReduceQuantityByOne();
            var unit = Instantiate(_unitToSpawn, position, Quaternion.identity, _playerUnitsParent);
            _unitsManager.AddPlayerUnitsWithoutRepeating(new List<GameObject> { unit.gameObject }, 1);
            unit.SetLevel(1);
            unit.InitUnitStats();
            unit.InitUnitTargets(_unitsManager.Units.Where(n=>n.Faction == Faction.Enemy).Select(n => n.transform).ToList());
            unit.UnitDied += _unitsManager.UnitDiedHandler;
            _battleReportRoot.AddUnitToBattleReport(unit);
        }

        private Ray GetRayUnderMouse()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = _sceneCamera.nearClipPlane;
            return _sceneCamera.ScreenPointToRay(mousePosition);
        }

        private void SpawnFlag(Vector3 position)
        {
            if (_spawnedFlag != null)
                Destroy(_spawnedFlag);
            _spawnedFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
        }

        internal void SpawnAllTroops()
        {
            StartCoroutine(SpawnAllTroopsCoroutine());
        }

        private IEnumerator SpawnAllTroopsCoroutine()
        {
            while (_coolDownUi.Quantity > 0)
            {
                SpawnUnitByFlag();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
