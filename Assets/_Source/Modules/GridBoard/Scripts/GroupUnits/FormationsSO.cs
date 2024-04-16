using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridBoard
{
    [CreateAssetMenu]
    internal class FormationsSO : ScriptableObject
    {
        [field: SerializeField] internal List<GroupUnitFormation> Formations { get; private set; }
        [SerializeField] private int _maxFormationLevel = 4;

        internal string[] GetFormation(bool isSingleCount, int level)
        {
            if (isSingleCount)
                return Formations[0].Positions;
            else
                return Formations[Mathf.Clamp(level, 1, _maxFormationLevel)].Positions;
        }
    }
}
