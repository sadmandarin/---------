using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class GroupUnit : MonoBehaviour
    {
        [SerializeField] private float _spaceBetweenUnits = 0.35f;
        [SerializeField] private FormationsSO _formations;
        [SerializeField] private UnitBoardBehaviorsSO _unitPrefabs;
        [SerializeField] private Transform _unitsParent;

        internal void Init(string name, int level)
        {
            GameObject unitToSpawn = _unitPrefabs.GetPrefabByName(name);
            string[] formation = _formations.GetFormation(_unitPrefabs.IsUnitSingleCount(name), level);
            for (int i = 0; i < formation.Length; i++)
            {
                string line = formation[i];
                var splittedLine = line.Split(',');
                for (int j = 0; j < splittedLine.Length; j++)
                {
                    string character = splittedLine[j];
                    if (character == "1")
                    {
                        var spawnedPrefab = Instantiate(unitToSpawn, _unitsParent);
                        var position = new Vector3(-_spaceBetweenUnits + j * _spaceBetweenUnits, 0,
                                                                            -_spaceBetweenUnits + i * _spaceBetweenUnits);
                        spawnedPrefab.transform.localPosition = position;
                    }
                }
            }
        }

        internal List<GameObject> GetUnitsInGroup()
        {
            List<GameObject> list = new List<GameObject>();
            foreach (Transform child in _unitsParent)
            {
                list.Add(child.gameObject);
            }
            return list;
        }
    }
}
