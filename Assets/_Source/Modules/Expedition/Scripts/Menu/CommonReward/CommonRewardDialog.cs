using DG.Tweening;
using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class CommonRewardDialog : MonoBehaviour
    {
        internal Action<ExtraRewardBase> OnDialogClosed;

        [SerializeField] private Transform _itemParent;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Text _itemQuantityText;
        [SerializeField] private Button _claimButton;
        [SerializeField] private CanvasGroup _claimCanvasGroup;
        [SerializeField] private float _timeToAnimate;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private ParticleSystem _particleSystem;

        private ExtraRewardBase _reward;

        internal void SetView(ExtraRewardBase reward, int quantity)
        {
            _reward = reward;
            var itemIcon = reward.Icon;

            _iconImage.sprite = itemIcon;
            _itemQuantityText.text = "x" + quantity;
        }

        internal void Animate()
        {
            _itemParent.transform.localScale = Vector3.one * 0.01f;
            DOTween.Sequence().Append(_itemParent.DOScale(1, _timeToAnimate))
                              .Join(_claimCanvasGroup.DOFade(1, _timeToAnimate))
                              .AppendCallback(_particleSystem.Play);
        }

        private void OnEnable()
        {
            _claimButton.onClick.AddListener(HandleClaimPresed);
            InitCamera();
        }

        private void InitCamera()
        {
            var cameraGameobject = GameObject.FindGameObjectWithTag("CanvasCamera");
            if (cameraGameobject.TryGetComponent(out Camera camera))
            {
                _canvas.worldCamera = camera;
            }
        }

        private void OnDisable()
        {
            _claimButton.onClick.RemoveListener(HandleClaimPresed);
        }

        private void HandleClaimPresed()
        {
            OnDialogClosed?.Invoke(_reward);
            Destroy(gameObject);
        }
    }
}
