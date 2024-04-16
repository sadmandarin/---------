using System;
using UnityEngine;

namespace GridBoard
{
    internal class BattleSlotUnitHolder : MonoBehaviour, IUnitHolder
    {
        public Action UnitPlaced, UnitRemoved;
        public IBoardUnit Unit => _unit;
        public bool HasUnit => _unit != null;

        public Vector2 Position {get; private set;}

        [SerializeField] private Transform _unitParent;

        private IBoardUnit _unit;

        internal void Init(Vector2 position)
        {
            Position = position;
        }

        public void DestroyUnit()
        {
            _unit.Destroy();
            SetNewUnit(null);
        }

        public void SetNewUnit(IBoardUnit newUnit)
        {
            if (newUnit == null)
            {
                _unit = null;
                UnitRemoved?.Invoke();
                return;
            }
            _unit = newUnit;
            _unit.Move(Position);
            newUnit.SetNewParent(_unitParent);
            UnitPlaced?.Invoke();
        }
    }

    public interface IUnitHolder
    {
        public IBoardUnit Unit { get; }
        public Vector2 Position { get; }
        public void SetNewUnit(IBoardUnit newUnit);
        public bool HasUnit { get; }

        public void DestroyUnit();
    }
}