namespace AutoBattler
{
    internal class AttackEventData
    {
        internal BattleReportID UnitThatAttacked { get; private set; }
        internal BattleReportID UnitThatWasAttacked { get; private set; }
        internal float EffectiveDamage { get; private set; }
        internal float BlockedDamage { get; private set; }

        internal AttackEventData(BattleReportID unitThatAttacked, BattleReportID unitThatWasAttacked, float effectiveDamage, float damageBlocked)
        {
            UnitThatAttacked = unitThatAttacked;
            UnitThatWasAttacked = unitThatWasAttacked;
            EffectiveDamage = effectiveDamage;
            BlockedDamage = damageBlocked;
        }
    }
}
