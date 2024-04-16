using System;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    [CreateAssetMenu(menuName ="Expedition/LevelRequirementsConfig")]
    internal class ConquestLevelRequirementsConfig : ScriptableObject
    {
        [field: SerializeField] public List<LevelRequirementData> Requirements { get; private set; }
    }

    [Serializable]
    internal struct SingleRequirementData
    {
        public float[] RequirementValues;
        public ConquestLevelRequirementBase Requirement;
    }

    [Serializable]
    internal struct LevelRequirementData
    {
        public SingleRequirementData[] Requirements;
    }
}
