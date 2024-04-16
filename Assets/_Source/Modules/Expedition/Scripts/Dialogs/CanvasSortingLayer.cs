using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class CanvasSortingLayer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Canvas canvas = GetComponent<Canvas>();

            canvas.sortingLayerName = "Default";
        }

    }
}
