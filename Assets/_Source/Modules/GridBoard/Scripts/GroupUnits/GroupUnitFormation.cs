using System;
using UnityEngine;

namespace GridBoard
{
    [Serializable]
    internal class GroupUnitFormation
    {
        [field: SerializeField] public int NumberOfUnits { get; private set; }
        [field: SerializeField] public string[] Positions { get; private set; } = new string[3];
    }
}
