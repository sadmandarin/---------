using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    public interface IBoardUnit
    {
        public void Init(string name, int level, Vector2 position);
        public string Name { get; }
        public int Level { get; }
        public Vector2 Position { get; }
        public bool InsideBarracks { get; }
        public void Move(Vector2 position);
        public void SetNewParent(Transform parent);

        public List<GameObject> GetAllUnitsInGroup();

        public IBoardUnit ReturnBoardUnit();

        public void StopBeingBoardUnit();

        public void Destroy();
    }
}
