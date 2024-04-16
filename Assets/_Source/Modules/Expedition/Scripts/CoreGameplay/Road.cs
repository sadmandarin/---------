using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class Road : MonoBehaviour
    {
        public CastleNode startCastle;

        public CastleNode endCastle;
        public Road(CastleNode start, CastleNode end)
        {
            startCastle = start;

            endCastle = end;
        }

        public void Awake()
        {

        }
    }
}
