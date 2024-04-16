using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public class ActiveSkillButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _fill;
        [SerializeField] private float _timeToStart;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _heroImage;

        private float _timer, _cooldownTime;

        private bool _timerTicking = false;
        private bool _isAutoSkillEnabled;

        internal void Init(float duration, Action action, Sprite heroIcon)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() =>
            {
                action.Invoke();
                StartCooldown();
            });
            _timerTicking = true;
            _button.enabled = false;
            _cooldownTime = duration;
            _timer = _cooldownTime - _timeToStart;
            _canvasGroup.alpha = 1;
            _heroImage.sprite = heroIcon;
        }

        internal void Hide()
        {
            _canvasGroup.alpha = 0;
            _particleSystem.gameObject.SetActive(false);
            _timerTicking = false;
        }

        internal void ToggleAutoSkill(bool activate)
        {
            _isAutoSkillEnabled = activate;
        }

        private void Update()
        {
            if (_timerTicking == false) return;

            _timer += Time.deltaTime;
            _fill.fillAmount = 1 - _timer / _cooldownTime;
            if (_timer >= _cooldownTime)
            {
                _button.enabled = true;
                _timerTicking = true;
                _particleSystem.gameObject.SetActive(true);

                if (_isAutoSkillEnabled)
                    _button?.onClick.Invoke();
            }
        }

        private void StartCooldown()
        {
            _timer = 0;
            _timerTicking = true;
            _particleSystem.gameObject.SetActive(false);
            _button.enabled = false;
        }
    }
}
