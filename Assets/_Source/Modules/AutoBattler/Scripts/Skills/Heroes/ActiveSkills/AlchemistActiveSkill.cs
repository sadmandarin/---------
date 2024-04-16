using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace AutoBattler
{
    public class AlchemistActiveSkill : ActiveHeroBaseSkill
    {
        [SerializeField] private AlchemistActiveSkillLevelsData[] _levels;
        [SerializeField] private Transform _heroPosition;
        [SerializeField] private float _range = 20f;
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private AlchemistPoisonCloud _posionCloudPrefab;

        private Faction _selfFaction;
        private AlchemistActiveSkillLevelsData _selectedLevel;
        private bool _alreadyFoundTarget;

        private GameObject _bomb, _poisonCloud;

        internal override void OnSkillActivated(int level, Faction selfFaction)
        {
            _selectedLevel = _levels[level];
            _selfFaction = selfFaction;
            _alreadyFoundTarget = false;

            SphereOverlapper.FindUnitsInsideSphereAndPerformAction(_range, _heroPosition.position, SelectTargetAndThrow);
        }

        private void SelectTargetAndThrow(AutoBattlerUnit unit)
        {
            if (_alreadyFoundTarget) return;

            if (unit.Faction != _selfFaction)
            {
                _alreadyFoundTarget = true;
                var bomb = Instantiate(_bombPrefab, _heroPosition.position, Quaternion.identity);
                _bomb = bomb;
                StartCoroutine(ProjectileShooter(bomb, unit));
            }
        }

        // More accurately shoots the projectile, could use refactoring
        private IEnumerator ProjectileShooter(GameObject bomb, AutoBattlerUnit unit)
        {
            for (int i = 0; i < 5; i++)
            {
                bomb.transform.DOJump(unit.transform.position, 0.5f, 1, 0.5f);
                yield return new WaitForSeconds(0.1f);
                bomb.transform.DOKill();
            }
            var poisonCloud = Instantiate(_posionCloudPrefab, bomb.transform.position, Quaternion.identity);
            DestroyBomb();
            poisonCloud.Init(_selectedLevel.Damage, _selectedLevel.EnemeyDefenseModifier, unit.Faction, BattleReportID);
            _poisonCloud = poisonCloud.gameObject;
        }

        private void OnDisable()
        {
            DestroyBomb();
            DestroyPoisonCloud();
        }

        private void DestroyBomb()
        {
            if (_bomb == null)
                return;
            Destroy(_bomb);
        }

        private void DestroyPoisonCloud()
        {
            if (_poisonCloud == null)
                return;
            Destroy(_poisonCloud);
        }
    }

    [Serializable]
    internal struct AlchemistActiveSkillLevelsData
    {
        public float Damage;
        public float EnemeyDefenseModifier;
    }
}
