using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    [Serializable]
    internal class BattleReportUnit
    {
        internal BattleReportID ID => _id;

        public float DamageTaken { get => _damageTaken; private set => _damageTaken = value; }
        public float DamageDealt { get => _damageDealt; private set => _damageDealt = value; }
        public float DamageBlocked { get => _damageBlocked; private set => _damageBlocked = value; }
        public float HealthHealed { get => _healthHealed; private set => _healthHealed = value; }
        public Sprite Icon { get => _icon; private set => _icon = value; }
        public bool IsHero { get => _isHero; private set => _isHero = value; }
        public int QuantityOfTroops { get => _quantityOfTroops; private set => _quantityOfTroops = value; }

        [SerializeField] private BattleReportID _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _damageTaken;
        [SerializeField] private float _damageDealt;
        [SerializeField] private float _damageBlocked;
        [SerializeField] private float _healthHealed;
        [SerializeField] private int _quantityOfTroops;
        
        private bool _isHero;

        internal BattleReportUnit(string name, int level, Faction faction, Sprite icon, int uniqueID, bool isHero)
        {
            _id = new BattleReportID(name, level, faction, uniqueID);
            _icon = icon;
            _isHero = isHero;
        }

        internal void IncreaseDamageTaken(float damageTaken)
        {
            _damageTaken += damageTaken;
        }

        internal void IncreaseDamageDealt(float effectiveDamage)
        {
            _damageDealt += effectiveDamage;
        }

        internal void IncreaseDamageBlocked(float damageBlocked)
        {
            _damageBlocked += damageBlocked;
        }

        internal void IncreaseHealthHealed(float healthHealed)
        {
            _healthHealed += healthHealed;
        }

        internal void IncreaseQuantityOfTroops()
        {
            _quantityOfTroops += 1;
        }
    }
}
