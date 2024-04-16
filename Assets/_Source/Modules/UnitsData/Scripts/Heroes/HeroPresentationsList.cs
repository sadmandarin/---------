using System.Collections.Generic;
using UnityEngine;

namespace UnitsData
{
    [CreateAssetMenu(menuName = "Heroes/HeroList")]
    public class HeroPresentationsList :ScriptableObject
    {
        [field: SerializeField] public List<HeroPresentationSO> Heroes { get; private set; }
    }
}
