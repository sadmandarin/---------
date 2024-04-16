using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    internal abstract class ConquestLevelRequirementBase : ScriptableObject
    {
        internal abstract string GetLevelRequirementText(float[] requirementValues);
        internal abstract bool IsRequirementFullfilled(float[] requirementValues, LevelController levelController);
    }
}
