using PersistentData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class AutoFightButton : MonoBehaviour
    {
        [SerializeField] private AutoFightAnimation _animation;
        [SerializeField] private Button _button;
        [SerializeField] private AutoFightController _controller;
        [SerializeField] private IntVariableSO _autoFight;
        [SerializeField] private Text _quantityText;
        [SerializeField] private GameObject _toggleTip;
        [SerializeField] private BoolVariableSO _purchasedAutoBattle;
        [SerializeField] private GameObject _textParent;

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleAutoFight);
            _autoFight.OnValueChanged += UpdateView;
            UpdateView();
            SetTip(false);
            if (_controller.Enabled)
                _animation.PlayAnimation();
            else
                _animation.StopAnimation();

        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleAutoFight);
            _autoFight.OnValueChanged -= UpdateView;
        }

        private void UpdateView(int value = 0)
        {
            if (_purchasedAutoBattle.Value)
            {
                _textParent.gameObject.SetActive(false);
                _quantityText.text = "";
            }
            else
            {
                _textParent.gameObject.SetActive(true);
                _quantityText.text = _autoFight.Value.ToString();
            }
             
        }

        internal void ToggleAutoFight()
        {
            if (_autoFight.Value > 0)
            {
                SetAutoFight(_controller.Enabled ? false : true);
            }
            else
            {
                ToggleTip();
            }
            
        }

        private void ToggleTip()
        {
            SetTip(_toggleTip.activeInHierarchy ? false : true);
        }

        private void SetTip(bool activate)
        {
            _toggleTip.SetActive(activate);
        }

        private void SetAutoFight(bool activate)
        {
            if (activate)
            {
                _controller.Activate();
                _animation.PlayAnimation();
            }
            else
            {
                _controller.Deactivate();
                _animation.StopAnimation();
            }
        }
    }
}
