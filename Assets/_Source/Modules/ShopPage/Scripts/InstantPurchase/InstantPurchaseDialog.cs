using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopPage
{
    internal class InstantPurchaseDialog : MonoBehaviour
    {
        [SerializeField] private List<ShopItemFloatVariableData> _shopItems;
        [SerializeField] private List<InstantPurchaseItem> _instantItem;
        [SerializeField] private Canvas _canvas;

        private void OnEnable()
        {
            //InitCamera();
            FillItemsWithData();
        }

        private void FillItemsWithData()
        {
            for (int i = 0; i < _shopItems.Count; i++)
            {
                _instantItem[i].SetUp(_shopItems[i]);
                _instantItem[i].OnItemPurchased += HandleItemPurchased;
            }
        }

        private void HandleItemPurchased()
        {
            Destroy(gameObject);
        }

        private void InitCamera()
        {
            var cameraGameobject = GameObject.FindGameObjectWithTag("CanvasCamera");
            if (cameraGameobject.TryGetComponent(out Camera camera))
            {
                _canvas.worldCamera = camera;
            }
        }
    }
}
