using System;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class TroopSpawnerCooldownUi : MonoBehaviour
    {
        internal Action OnCooldownEnded;
        internal int Quantity => _quantity;

        public Action OnButtonPressed { get; internal set; }

        [SerializeField] private Image _fill;
        [SerializeField] private float _timeToStart;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private int _startingQuantity;
        [SerializeField] private Text _quantityText;
        [SerializeField] private Button _button;

        private float _timer;
        private bool _timerTicking = false;
        private bool _isAutoSkillEnabled;
        private int _quantity = 3;

        internal void ToggleAutoSkill(bool activate)
        {
            _isAutoSkillEnabled = activate;
        }

        internal void Init()
        {
            _timerTicking = true;
            _timer = _cooldownTime - _timeToStart;
            _canvasGroup.alpha = 1;
            _quantity = _startingQuantity;
            SetQuantityText();
        }

        private void SetQuantityText()
        {
            _quantityText.text = "X" + _quantity;
        }

        internal void ReduceQuantityByOne()
        {
            _quantity -= 1;
            SetQuantityText();
        }

        private void Update()
        {
            if (_timerTicking == false) return;

            _timer += Time.deltaTime;
            _fill.fillAmount = 1 - _timer / _cooldownTime;
            if (_timer >= _cooldownTime)
            {
                _timerTicking = true;
                _quantity += 1;
                SetQuantityText();
                StartCooldown();
                if (_isAutoSkillEnabled)
                    TryToSpawnUnit();
            }
        }

        private void StartCooldown()
        {
            _timer = 0;
            _timerTicking = true;
        }

        internal void Stop()
        {
            _timerTicking = false;
        }

        internal void Hide()
        {
            _canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(TryToSpawnUnit);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(TryToSpawnUnit);
        }

        private void TryToSpawnUnit()
        {
            if (_quantity > 0)
            {
                OnButtonPressed?.Invoke();
            }
        }
    }
}
