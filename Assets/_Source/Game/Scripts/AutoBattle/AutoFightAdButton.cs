using PersistentData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class AutoFightAdButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private IntVariableSO _autoFightTimes;
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private AutoFightController _controller;
        [SerializeField] private AutoFightAnimation _animation;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleOnAdButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnAdButtonClicked);
        }

        private void HandleOnAdButtonClicked()
        {
            YandexManager.Instance.WatchRewardedWithoutClicker(ClaimReward);
        }

        private void ClaimReward()
        {
            _autoFightTimes.Value += 10;
            _parentObject.SetActive(false);
            _controller.Activate();
            _animation.PlayAnimation();
        }
    }
}
