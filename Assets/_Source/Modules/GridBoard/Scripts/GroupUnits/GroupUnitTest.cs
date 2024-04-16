using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class GroupUnitTest : MonoBehaviour
    {
        [SerializeField] private GroupUnit _groupUnit;
        [SerializeField] private string _name;
        [SerializeField] private int _level;
        [SerializeField] private FormationsSO _formations;
        [SerializeField] private GameObject _unitPrefab;


        [ContextMenu(nameof(Test))]
        internal void Test()
        {
            _groupUnit.Init(_name, _level);
        }
    }
}
