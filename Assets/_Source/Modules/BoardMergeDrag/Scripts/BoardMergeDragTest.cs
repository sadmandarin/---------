using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardMergeDrag
{
    public class BoardMergeDragTest : MonoBehaviour
    {
        [SerializeField] private BoardMergeDragRoot _boardMergeDragRoot;

        private void Start()
        {
            _boardMergeDragRoot.Init();
        }
    }
}
