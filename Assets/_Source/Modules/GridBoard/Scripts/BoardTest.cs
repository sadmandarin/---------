using PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class BoardTest : MonoBehaviour
    {
        [SerializeField] private List<BoardUnitData> _unitDatas;
        [SerializeField] BoardRoot _boardRoot;
        [SerializeField] private BoardUnit _unit1;
        [SerializeField] private BoardUnit _unit2;
        [SerializeField] private BattleSlotUnitHolder _unitHolder1;
        [SerializeField] private BattleSlotUnitHolder _unitHolder2;
        [SerializeField] private BoardUnitsPersistentHolder _boardUnits;

        private void Start()
        {
            _boardRoot.Init(5, 5);
            BoardUnit[] boardUnits = new BoardUnit[2];
            boardUnits[0] = _unit1;
            boardUnits[1] = _unit2;
            _boardRoot.Load(_boardUnits.Units.ToArray());
        }

        [ContextMenu(nameof(Change))]
        public void Change()
        {
            _boardRoot.MoveToNewSlot(_unitHolder1, _unitHolder2, out bool willBeUpgraded);
        }

        [ContextMenu(nameof(UnitsOnBoardTest))]
        public void UnitsOnBoardTest()
        {
            var units = _boardRoot.GetAllUnitsOnBoard();
            foreach (var unit in units)
            {
                Debug.Log(unit.gameObject.name);
            }
        }

        [ContextMenu(nameof(StopUnitBehaviour))]
        public void StopUnitBehaviour()
        {
            _boardRoot.DeactivateBoardUnitBehaviour();
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //_boardRoot.StartBoardUpdate(_unitHolder1);
                return;
            }

            if (Input.GetMouseButton(0))
            {
                _boardRoot.BoardUpdate();
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _boardRoot.StopBoardUpdate(false, out bool willBeUpgraded);
                return;
            }
        }
    }
}
