using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    internal class ProgressRewardDetail : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Text _quantityText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _button;

        private ExtraRewardBase _extraReward;
        private int _quantity;

        internal void SetUp(ExtraRewardBase extraReward, int quantity)
        {
            _extraReward = extraReward;
            _quantity = quantity;

            extraReward.GetRewardDescripton(out Sprite icon, out string description);
            _iconImage.sprite = icon;
            _quantityText.text = quantity.ToString();
            _descriptionText.text = description;
        }

        private void OnEnable()
        {
            InitCamera();
            _button.onClick.AddListener(ClaimReward);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ClaimReward);
        }

        private void InitCamera()
        {
            var cameraGameobject = GameObject.FindGameObjectWithTag("CanvasCamera");
            if (cameraGameobject.TryGetComponent(out Camera camera))
            {
                _canvas.worldCamera = camera;
            }
        }

        private void ClaimReward()
        {
            if (_extraReward == null)
                throw new InvalidOperationException();

            _extraReward.ClaimReward(_quantity);
        }
    }
}
