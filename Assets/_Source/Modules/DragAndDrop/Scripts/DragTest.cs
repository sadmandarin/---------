using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{
    public class DragTest : MonoBehaviour
    {
        [SerializeField] private DragRoot _dragRoot;

        private void Start()
        {
            _dragRoot.Init();

            _dragRoot.Activate();
        }
    }
}
