using System.Collections;
using UnityEngine;
using AutoBattler;
using GridBoard;
using Merge2;
using DragAndDrop;
using System.Collections.Generic;
using System;
using PersistentData;
using UnitsData;
using DG.Tweening;
using Quests;

public class AutoBattlerAndBoardRoot : MonoBehaviour
{
    [SerializeField] private DragRoot _dragRoot;
    [SerializeField] private MergeRoot _mergeRoot;
    [SerializeField] private AutoBattlerRoot _autoBattlerRoot;
    [SerializeField] private BoardRoot _boardRoot;
    [SerializeField] private BarracksRoot _barracksRoot;
    [SerializeField] private List<BoardUnitData> _boardData;
    [SerializeField] private List<BarracksUnitData> _barracksData;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private BoardUnitsPersistentHolder _boardUnitsPersistentHolder;
    [SerializeField] private BarracksUnitsPersistentHolder _barracksUnitsPersistentHolder;
    [SerializeField] private UnitViewsListSO _unitViewsList;
    [SerializeField] private LevelConfigBaseSO _levelConfig;
    [SerializeField] private QuestsCompleter _questsCompleter;
    [SerializeField] private QuestItemDescriptionSO _quest;
    [SerializeField] private GameObject _difficulty, _newCardController;

    public void Init(LevelConfigBaseSO levelConfig)
    {
        _levelConfig = levelConfig;

        _autoBattlerRoot.InitHero(_boardRoot.HeroPositionParent);
        _autoBattlerRoot.Init(_levelConfig);

        _boardRoot.EnableBoard();
        _boardUnitsPersistentHolder.UpdateBoardSize();
        _boardRoot.Init(_boardUnitsPersistentHolder.BoardSize.x, _boardUnitsPersistentHolder.BoardSize.y);
        _boardRoot.Load(_boardUnitsPersistentHolder.Units.ToArray());
        _boardRoot.UnitsOnBoardChanged += UpdatePlayerUnits;

        _barracksRoot.LoadBarracksUnits(_barracksUnitsPersistentHolder.Units);
        _barracksRoot.OnCellPressed += CellPressedHandler;

        _dragRoot.Init();
        _dragRoot.Activate();

        _dragRoot.StartedDragging += StartedDraggingHandler;
        _dragRoot.Dropped += HandleDropped;

        
        UpdatePlayerUnits();

        //_mergeRoot.Merged += MergedHandler;
        _barracksRoot.UnitInsideBarracksChanged += UnitInsideBarracksChangedHandler;
        _autoBattlerRoot.OnBattleStarted += BattleStartedHandler;

        _difficulty.SetActive(levelConfig.IsMission == false);
        _newCardController.SetActive(levelConfig.IsMission == false);
    }

    public void ResetBattle()
    {
        //_boardRoot.EnableBoard();
        _boardRoot.DisableBoard();
        _autoBattlerRoot.ResetBattle();
        _barracksRoot.ClearCellsInBarracks();
        _dragRoot.Deactivate();
        _newCardController.SetActive(false);

        _barracksRoot.OnCellPressed -= CellPressedHandler;
        _dragRoot.StartedDragging -= StartedDraggingHandler;
        _dragRoot.Dropped -= HandleDropped;
        _barracksRoot.UnitInsideBarracksChanged -= UnitInsideBarracksChangedHandler;
        _autoBattlerRoot.OnBattleStarted -= BattleStartedHandler;
    }

    private void UnitInsideBarracksChangedHandler(bool isInside)
    {
        _boardRoot.ToggleArmyTopInfoVisibility(!isInside);
    }

    private void StartedDraggingHandler(GameObject obj)
    {
        if (obj.TryGetComponent(out IBoardUnit boardUnit))
        {
            _boardRoot.StartBoardUpdate(boardUnit);
            _boardRoot.Activate();
        }
    }

    private void CellPressedHandler(IBarracksTroopCell cell)
    {
        if (_boardRoot.CanAddNewUnitsToBoard == false)
            return;

        _barracksRoot.RemoveUnitFromBarracks(cell);
        _boardRoot.AddUnitToFreeSpot(cell.Name, cell.Level);
        
    }

    private void HandleDropped(GameObject obj)
    {
        var returnedToBarracks = TryReturnToBarracks(obj);

        _boardRoot.StopBoardUpdate(returnedToBarracks, out bool willBeUpgraded);
        _boardRoot.Deactivate();
        

        bool merged = false;
        

        if (willBeUpgraded && returnedToBarracks == false)
        {
            merged = TryMerge(obj);
        }
        
        
        if (merged)
            _questsCompleter.CompleteQuest(_quest);
        if (merged || returnedToBarracks)
            UpdatePlayerUnits();
    }

    private bool TryMerge(GameObject obj)
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
                break;
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
            merged = _mergeRoot.TryMerge(itemsToMerge[0], itemsToMerge[1], out newMergableItem);
        }
        if (merged)
        {

            if (newMergableItem.GetItem.TryGetComponent(out IBoardUnit boardUnit))
            {
                boardUnit.Init(oldboardUnit.Name, oldboardUnit.Level + 1, oldboardUnit.Position);
                _boardRoot.SetUnitInLastSelectedSlot(boardUnit);
                return true;
            }
        }
        return false;
    }

    private bool TryReturnToBarracks(GameObject droppedObject)
    {
        if (droppedObject == null)
            return false;
        if (_boardRoot.CanRemoveUnitFromBoard == false)
            return false;

        var addedToBarracks = _barracksRoot.TryAddToBarracks(droppedObject);
        if (addedToBarracks)
        {
            _boardRoot.RemoveUnitFromBoard(droppedObject.GetComponent<IBoardUnit>());
            return true;
        }
        return false;
    }

    private void UpdatePlayerUnits()
    {
        StartCoroutine(UnitsOnBoardUpdater());
    }

    private IEnumerator UnitsOnBoardUpdater()
    {
        // You need to wait a frame for items (lower tier) to get destroyed after merge
        yield return new WaitForEndOfFrame();
        var allBoardUnits = _boardRoot.GetAllBoardUnits();
        foreach (var group in allBoardUnits)
        {
            _autoBattlerRoot.AddPlayerUnits(group.GetAllUnitsInGroup(), group.Level);
        }
        _autoBattlerRoot.UpdatePlayerUnitsStats();
    }

    private void BattleStartedHandler()
    {
        _boardRoot.DisableBoard();
    }
}
