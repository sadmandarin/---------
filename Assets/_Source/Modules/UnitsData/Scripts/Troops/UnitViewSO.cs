using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnitsData
{
    [CreateAssetMenu(menuName = "Units/UnitView")]
    public class UnitViewSO : ScriptableObject
    {
        [field: SerializeField] public TroopName Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Rarity { get; private set; }
    }

    public enum TroopName
    {
        Infantry,
        Archer,
        IronGuard,
        Bomber,
        Bandits,
        OgreWarrior,
        FireMage,
        IceMage,
        DeadlyAssassins,
        Catapult,
        MagicApprentice,
        HellJailer,
        Paladin,
        Scholar,
        Inquisitor,
        UndeadSoldier
    }
}



