using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class CanvasSwitcher : MonoBehaviour
    {
        [SerializeField] private Canvas _beforeBattleCanvas;
        [SerializeField] private Canvas _duringBattleCanvas;
        [SerializeField] private GameObject[] _objectsToMoveBetweenCanvases;

        internal void ResetCanvases()
        {
            foreach (var objectToMove in _objectsToMoveBetweenCanvases)
            {
                objectToMove.transform.SetParent(_beforeBattleCanvas.transform, false);
            }
            _beforeBattleCanvas.gameObject.SetActive(true);
            _duringBattleCanvas.gameObject.SetActive(false);
        }

        internal void SwitchToBattleCanvas()
        {
            foreach (var objectToMove in _objectsToMoveBetweenCanvases)
            {
                objectToMove.transform.SetParent(_duringBattleCanvas.transform, false);
            }
            _beforeBattleCanvas.gameObject.SetActive(false);
            _duringBattleCanvas.gameObject.SetActive(true);
        }

        internal void SwitchToEndCanvas()
        {
            _beforeBattleCanvas.gameObject.SetActive(false);
            _duringBattleCanvas.gameObject.SetActive(false);
        }
    }
}
