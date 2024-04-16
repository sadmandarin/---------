using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GridBoard
{
    internal class BattleSlot : MonoBehaviour
    {
        public BattleSlotUnitHolder UnitHolder => _unitHolder;
        public Vector2 Position { get; private set; }

        [SerializeField] private BattleSlotVisualizer _visualizer;
        [SerializeField] private BattleSlotUnitHolder _unitHolder;

        internal bool IsActive { get; private set; }

        internal void Init(Vector2 position, bool isActive)
        {
            Position = position;
            _unitHolder.Init(position);

            IsActive = isActive;
            if (isActive)
            {
                
                _unitHolder.UnitPlaced += () => _visualizer.ToggleCross(false);
                _unitHolder.UnitRemoved += () => _visualizer.ToggleCross(true);
            }
            else
            {
                _visualizer.SetLockedMaterial();
                _visualizer.ToggleCross(false);
            }
            
        }

        internal void Load(IBoardUnit boardUnit)
        {
            _unitHolder.SetNewUnit(boardUnit.ReturnBoardUnit());
            _visualizer.ToggleCross(false);
        }

        internal void Free()
        {
            _unitHolder.DestroyUnit();
        }

        internal void Select() => _visualizer.Select();
        internal void Unselect() => _visualizer.Unselect();
        internal void ToggleAvailableEffect(bool value) => _visualizer.ToggleAvailableForUpgradeEffect(value);
        internal void PlayUpgradeEffect() => _visualizer.PlayUpgradeEffect();
        
    }
}
