using DragAndDrop;
using GridBoard;
using Merge2;
using PersistentData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoardMergeDrag
{
    public class BoardMergeDragRoot : MonoBehaviour
    {
        public Action Merged;
        public BoardRoot BoardRoot => _boardRoot;


        [SerializeField] private DragRoot _dragRoot;
        [SerializeField] private BoardRoot _boardRoot;
        [SerializeField] private BarracksRoot _barracksRoot;
        [SerializeField] private MergeRoot _mergeSystem;
        [SerializeField] private GameObject[] _boardUnits;
        [SerializeField] private List<BoardUnitData> _boardData;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private BoardUnitsPersistentHolder _boardUnitsPersistentHolder;
        [SerializeField] private BarracksUnitsPersistentHolder _barracksHolder;
        public void Init()
        {
            _boardRoot.Init(5, 5);
            _boardRoot.Load(_boardUnitsPersistentHolder.Units.ToArray());
            _barracksRoot.LoadBarracksUnits(_barracksHolder.Units);
            _barracksRoot.OnCellPressed += CellPressedHandler;
            _dragRoot.Init();
            _dragRoot.Activate();
            _dragRoot.StartedDragging += HandleStartDrag;
            _dragRoot.Dropped += HandleDropped;
        }

        private void CellPressedHandler(IBarracksTroopCell cell)
        {
            if (_boardRoot.CanAddNewUnitsToBoard == false)
                return;

            _barracksRoot.RemoveUnitFromBarracks(cell);
            _boardRoot.AddUnitToFreeSpot(cell.Name, cell.Level);
        }


        public List<GameObject> GetAllUnitsOnBoard()
        {
            return _boardRoot.GetAllUnitsOnBoard();
        }

        public void DeactivateBoard()
        {
            _boardRoot.DisableBoard();
        }

        private void HandleStartDrag(GameObject obj)
        {
            if (obj.TryGetComponent(out IBoardUnit boardUnit))
            {
                _boardRoot.StartBoardUpdate(boardUnit);
                _boardRoot.Activate();
            }
        }

        private void HandleDropped(GameObject obj)
        {
            _boardRoot.StopBoardUpdate(false, out bool willBeUpgraded);
            _boardRoot.Deactivate();

            TryMerge(obj);
            TryReturnToBarracks(obj);
        }

        private void TryReturnToBarracks(GameObject obj)
        {
            if (obj == null)
                return;
            if (_boardRoot.CanRemoveUnitFromBoard)
                _barracksRoot.TryAddToBarracks(obj);
            var addedToBarracks = _barracksRoot.TryAddToBarracks(obj);
            if (addedToBarracks)
            {
                _boardRoot.RemoveUnitFromBoard(obj.GetComponent<IBoardUnit>());
            }
        }

        private void TryMerge(GameObject obj)
        {
            RaycastHit[] hits = new RaycastHit[2];
            bool canMerge = true;
            bool merged = false;
            IMergableItem[] itemsToMerge = new IMergableItem[2];
            Physics.SphereCastNonAlloc(obj.transform.position, 0.5f, Vector3.up, hits, 1, _layerMask);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform == null)
                {
                    canMerge = false;
                    return;
                }
                if ((hits[i].transform.TryGetComponent(out IMergableItem mergableItem) == false))
                {
                    canMerge = false;
                    break;
                }
                itemsToMerge[i] = mergableItem;
            }
            IMergableItem newMergableItem = null;
            obj.TryGetComponent(out IBoardUnit oldboardUnit);
            if (canMerge)
            {
                merged = _mergeSystem.TryMerge(itemsToMerge[0], itemsToMerge[1], out newMergableItem);
            }
            if (merged)
            {
                
                if (newMergableItem.GetItem.TryGetComponent(out IBoardUnit boardUnit))
                {
                    boardUnit.Init(oldboardUnit.Name, oldboardUnit.Level + 1, oldboardUnit.Position);
                    _boardRoot.SetUnitInLastSelectedSlot(boardUnit);
                    Merged?.Invoke();
                }
            }
        }
    }
}
