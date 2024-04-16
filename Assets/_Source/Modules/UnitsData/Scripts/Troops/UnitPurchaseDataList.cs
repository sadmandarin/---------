using System.Collections.Generic;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Units/PurchaseDataList")]
    public class UnitPurchaseDataList : ScriptableObject
    {
        [field: SerializeField] public List<UnitPurchaseDataSO> UnitPurchaseData { get; private set; }
    }
}
