using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class CastleNode : MonoBehaviour
    {
        public GameObject castle;

        public Vector3 position { get; }

        public List<CastleNode> connectedCastles;

        public CastleNode(GameObject castle, Vector3 pos)
        {
            this.castle = castle;

            position = pos;

            connectedCastles = new List<CastleNode>();
        }

        private void Awake()
        {
            
        }
    }
}
