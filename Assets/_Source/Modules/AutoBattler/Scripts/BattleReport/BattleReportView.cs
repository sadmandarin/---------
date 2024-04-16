using UnityEngine;

namespace AutoBattler
{
    internal class BattleReportView : MonoBehaviour
    {
        internal Sprite UnitIcon => _unitIcon;
        internal string Name => _name;
        internal int Level => _level;
        internal Faction Faction => _faction;
        internal int UniqueID => _uniqueID;

        internal BattleReportID ID { get; private set; }

        

        [SerializeField] private Sprite _unitIcon;
        private string _name;
        private int _level;
        private Faction _faction;
        private int _uniqueID;

        internal void Init(string name, int level, Faction faction, int uniqueID)
        {
            _name = name;
            _level = level;
            _faction = faction;
            _uniqueID = uniqueID;
            ID = new BattleReportID(_name, _level, _faction, _uniqueID);
        }

        
    }
}
