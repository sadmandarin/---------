using System;

namespace AutoBattler
{
    [Serializable]
    internal struct DoubleSidedModifier
    {
        public StatsModifierBase AllyBuffs;
        public StatsModifierBase EnemyDebuffs;
    }
}
