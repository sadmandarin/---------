using System;

namespace PersistentData
{
    [Serializable]
    public struct BarracksUnitData
    {
        public string Name;
        public int Level;

        public BarracksUnitData(string name, int level)
        {
            Name = name;
            Level = level;
        }
    }
    
}
