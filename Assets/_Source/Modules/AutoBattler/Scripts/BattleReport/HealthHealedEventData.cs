namespace AutoBattler
{
    internal class HealthHealedEventData
    {
        internal BattleReportID UnitThatHealed{ get; private set; }
        internal float HealthHealed { get; private set; }

        public HealthHealedEventData(BattleReportID unitThatHealed, float healthHealed)
        {
            UnitThatHealed = unitThatHealed;
            HealthHealed = healthHealed;
        }
    }
}
