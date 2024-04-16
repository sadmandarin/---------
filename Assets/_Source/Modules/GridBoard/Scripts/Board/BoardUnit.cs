using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class BoardUnit : MonoBehaviour, IBoardUnit
    {
        public Vector2 Position => _position;
        public string Name => _name;
        public int Level => _level;
        public bool InsideBarracks => _barracksUnit.InsideBarracks;

        [SerializeField] private string _name;
        [SerializeField] private int _level;
        [SerializeField] private Vector2 _position;
        [SerializeField] private GroupUnit _groupUnit;
        [SerializeField] private BarracksUnit _barracksUnit;
        [SerializeField] private BoxCollider _boxCollider;

        public void Move(Vector2 position)
        {
            _position = position;
        }

        public void SetNewParent(Transform parent)
        {
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
        }

        public IBoardUnit ReturnBoardUnit()
        {
            return this;
        }

        public void Init(string name, int level, Vector2 position)
        {
            _name = name;
            _level = level;
            _position = position;
            _groupUnit.Init(name, level);
            _barracksUnit.Init(name, level);
        }

        public List<GameObject> GetAllUnitsInGroup()
        {
            return _groupUnit.GetUnitsInGroup();
        }

        public void StopBeingBoardUnit()
        {
            gameObject.layer = 0;
            _boxCollider.enabled = false;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
