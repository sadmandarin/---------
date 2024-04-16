using System;
using UnityEngine;

namespace PersistentData
{
    [Serializable]
    public struct BoardUnitData
    {
        public string Name;
        public int Level;
        public Vector2 Position;

        public BoardUnitData(string name, int level, Vector2 position)
        {
            Name = name;
            Level = level;
            Position = position;
        }
    }
}
