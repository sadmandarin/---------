using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class BarracksUnit : MonoBehaviour
    {
        internal bool InsideBarracks { get => _insideBarracks; private set => _insideBarracks = value; }
        internal string Name => _name;
        internal int Level => _level;

        [SerializeField] private BarracksTroopCell _troopCellPrefab;
        [SerializeField] private BarracksTroopCell _troopCellPrefabForBarracks;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private TroopsViewsSO _barracksTroopsViews;

        private BarracksTroopCell _cell;
        private bool _insideBarracks;
        private int _level;
        private string _name;
        private int _rarity;

        internal void Init(string name, int level)
        {
            _level = level;
            _name = name;
        }

        internal BarracksTroopCell GetTroopCell()
        {
            var barracksView = _barracksTroopsViews.GetTroopViewByName(_name);
            var cell = Instantiate(_troopCellPrefab);
            
            var canvas = FindObjectOfType<BarracksCellsCanvas>().transform;
            cell.transform.SetParent(canvas);
            Debug.Log("Parent of cell" + canvas.gameObject.name);
            cell.Init(barracksView.Icon, barracksView.rarity, _level, _name);
            return cell;
        }

        private void OnDestroy()
        {
            if (_cell != null)
                Destroy(_cell.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MovedToBarracksTrigger trigger))
            {
                _insideBarracks = true;
                _cell = GetTroopCell();
                _cell.StartFollowingMouse();
                _unitsParent.gameObject.SetActive(false);
                trigger.InvokeSomeoneInsideBarracksStateChanged(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MovedToBarracksTrigger trigger))
            {
                _insideBarracks = false;
                Destroy(_cell.gameObject);
                _unitsParent.gameObject.SetActive(true);
                trigger.InvokeSomeoneInsideBarracksStateChanged(false);
            }
        }
    }
}
