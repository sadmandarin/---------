using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "PersistentHolders/BarracksUnits")]
    public class BarracksUnitsPersistentHolder : UntisPersistentHolder<BarracksUnitData>
    {
        public void InitWithStartingData()
        {
            Units.Clear();
        }

        public bool IsUnitInBarracks(string name)
        {
            var unitInList = Units.Where(n => n.Name == name);
            return unitInList.Count() > 0;
        }
    }
}
