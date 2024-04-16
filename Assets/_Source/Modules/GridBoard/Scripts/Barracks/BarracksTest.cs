using UnityEngine;

namespace GridBoard
{
    internal class BarracksTest : MonoBehaviour
    {
        [SerializeField] private Sprite _iconSprite;
        [SerializeField] private string _name;
        [SerializeField] private int _level;
        [SerializeField] private BarracksTroopCell _troopPrefab;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private BarracksUnit _unit;
        [SerializeField] private BarracksRoot _barracksRoot;
        [SerializeField] private BoardRoot _boardRoot;
        [SerializeField] private BarracksTroopCell _cell;

        [ContextMenu(nameof(AddUnit))]
        internal void AddUnit()
        {
            //_barracksRoot.AddUnitToBarracks(_unit);
        }

        [ContextMenu(nameof(AddUnitToBoardByName))]
        internal void AddUnitToBoardByName()
        {
            _boardRoot.AddUnitToFreeSpot(_name, _level);
        }

        [ContextMenu(nameof(AddUnitToBoardByCell))]
        internal void AddUnitToBoardByCell()
        {
            _barracksRoot.RemoveUnitFromBarracks(_cell);
        }
    }
}
