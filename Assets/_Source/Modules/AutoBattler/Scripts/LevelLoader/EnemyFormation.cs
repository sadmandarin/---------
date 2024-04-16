using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    [Serializable]
    internal class EnemyFormation
    {
        internal int NumberOfUnitsInARow => _numberOfUnitsInARow;
        internal string[] Rows => _rows;

        [SerializeField] private int _numberOfUnitsInARow;
        [SerializeField] private string[] _rows;

        internal EnemyFormation(int numberOfUnitsInARow, string[] rows)
        {
            _numberOfUnitsInARow = numberOfUnitsInARow;
            _rows = rows;
        }
    }
}
