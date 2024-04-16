using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroStatsDescriptionTipButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private HeroStatsDescriptionBubble _description;

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleDescription);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleDescription);
        }

        private void ToggleDescription()
        {
            _description.ToggleVisibility();
        }
    }
}
