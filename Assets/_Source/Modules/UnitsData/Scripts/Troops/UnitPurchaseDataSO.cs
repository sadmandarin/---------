using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Units/PurchaseData")]
    public class UnitPurchaseDataSO : ScriptableObject
    {
        [field: SerializeField] public UnitViewSO UnitView;
        [field: SerializeField] public int LevelRequirement;
    }
}
