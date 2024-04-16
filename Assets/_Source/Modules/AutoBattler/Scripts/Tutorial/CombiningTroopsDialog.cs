using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class CombiningTroopsDialog : MonoBehaviour
    {
        [SerializeField] Canvas _canvas;

        internal void InitDialog(Camera canvasCamera)
        {
            _canvas.worldCamera = canvasCamera;
        }
    }
}
