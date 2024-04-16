using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GridBoard
{
    internal class CardSelectScrollRectSnap : MonoBehaviour, IEndDragHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _content;
        [SerializeField] private float _timeToSnap = 0.5f;
        [SerializeField] private CardDrawerSelectDialog _dialog;

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("content anchored position " + _content.anchoredPosition.x);
            float scrollPosition = _content.anchoredPosition.x;
            float closestPosition = -_content.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x;
            int savedIndex = 0;
            foreach (RectTransform child in _content)
            {
                var position = child.anchoredPosition.x;
                if (Mathf.Abs(-position - scrollPosition) < Mathf.Abs(-closestPosition - scrollPosition))
                {
                    closestPosition = position;
                    savedIndex = child.GetSiblingIndex();
                }
                Debug.Log(child.anchoredPosition.x);
            }
            _scrollRect.StopMovement();
            _content.DOAnchorPosX(-closestPosition, _timeToSnap);
            _dialog.SelectCard(savedIndex);
        }

        public void SnapToElement(int index)
        {
            float position = -_content.GetChild(index).GetComponent<RectTransform>().anchoredPosition.x;
            _content.DOAnchorPosX(position, _timeToSnap);
        }
    }
}
