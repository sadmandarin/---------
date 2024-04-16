using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoBattler
{
    public class StrengthBalanceBubble : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private StrengthBalanceInfo _topInfo;
        public void OnPointerDown(PointerEventData eventData)
        {
            _topInfo.ShowStats();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _topInfo.HideStats();
        }
    }
}
