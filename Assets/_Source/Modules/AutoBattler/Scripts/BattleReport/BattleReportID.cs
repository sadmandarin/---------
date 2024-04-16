using System;

namespace AutoBattler
{
    [Serializable]
    public struct BattleReportID
    {
        internal string Name;
        internal int Level;
        internal Faction Faction;
        internal int UniqueID;

        internal BattleReportID(string name, int level, Faction faction, int uniqueID)
        {
            Name = name;
            Level = level;
            Faction = faction;
            UniqueID = uniqueID;
        }

        public static bool operator ==(BattleReportID a, BattleReportID b)
        {
            if (a.Name == b.Name && a.Level == b.Level && a.Faction == b.Faction && a.UniqueID == b.UniqueID)
                return true;
            else
                return false;
        }

        public static bool operator !=(BattleReportID a, BattleReportID b)
        {
            if (a.Name == b.Name && a.Level == b.Level && a.Faction == b.Faction && a.UniqueID == b.UniqueID)
                return false;
            else
                return true;
        }

        internal bool SameTroopType(BattleReportID secondTroop)
        {
            return (Name == secondTroop.Name && Level == secondTroop.Level && this.Faction == secondTroop.Faction);
        }
    }
}
