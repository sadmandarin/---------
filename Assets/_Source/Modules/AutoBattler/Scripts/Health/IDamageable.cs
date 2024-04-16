namespace AutoBattler
{
    public interface IDamageable
    {
        void TakePhysicalHit(BattleReportID attackingUnitID, float damage);
        void TakeMagicHit(BattleReportID attackingUnitID, float damage);    
    }
}
