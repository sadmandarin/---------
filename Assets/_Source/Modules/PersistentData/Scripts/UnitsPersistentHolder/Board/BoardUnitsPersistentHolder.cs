using System.Collections;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "PersistentHolders/BoardUnits")]
    public class BoardUnitsPersistentHolder : UntisPersistentHolder<BoardUnitData>
    {
        [field:SerializeField] public Vector2Int BoardSize { get; private set; }
        [SerializeField] private LevelVariable _mainLevel;
        [SerializeField] private BoardSizeConfig _boardSizeConfig;
        

        public void UpdateBoardSize()
        {
            BoardSize = _boardSizeConfig.GetSize(_mainLevel.Value);
        }

        public void InitWithStartingData()
        {
            Units.Clear();
            Units.Add(new BoardUnitData("Infantry", 1, new Vector2(3, 6)));
;        }
        public bool IsUnitOnBoard(string name)
        {
            var unitInList = Units.Where(n => n.Name == name);
            return unitInList.Count() > 0;
        }

        public bool IsThereAFreeSpotOnBoard()
        {
            UpdateBoardSize();

            int[,] activeSlots = BoardSizeConfig.GetActiveSlots(BoardSize.x, BoardSize.y);
            for (int i = 6; i >= 0; i--)
            {
                for (int j = 6; j >= 0; j--)
                {
                    if (activeSlots[i, j] == 0)
                        continue;

                    if (Units.Any(n => n.Position == new Vector2(i, j)) == false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddUnitToFreeSpot(string name, int level)
        {
            UpdateBoardSize();

            int[,] activeSlots = BoardSizeConfig.GetActiveSlots(BoardSize.x, BoardSize.y);
            for (int i = 6; i >= 0; i--)
            {
                for (int j = 6; j >= 0; j--)
                {
                    if (activeSlots[i, j] == 0)
                        continue;

                    if (Units.Any(n => n.Position == new Vector2(i, j)) == false)
                    {
                        AddUnit(new BoardUnitData(name, level, new Vector2(i, j)));
                        return;
                    }
                }
            }
        }

        public void HandleUpgrade(BoardUnitData unit1, BoardUnitData unit2)
        {
            RemoveUnit(unit2);
            RemoveUnit(unit1);
            AddUnit(new BoardUnitData(unit1.Name, unit1.Level + 1, unit1.Position));
        }

        public void HandleUnitsChangedPlaces(BoardUnitData unit1, BoardUnitData unit2)
        {
            RemoveUnit(unit2);
            RemoveUnit(unit1);
            AddUnit(new BoardUnitData(unit2.Name,   unit2.Level, unit1.Position));
            AddUnit(new BoardUnitData(unit1.Name, unit1.Level, unit2.Position));
        }

        
    }
}
