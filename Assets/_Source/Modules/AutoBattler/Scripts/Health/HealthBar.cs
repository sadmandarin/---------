using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private Health _health;

        private Camera _cam;

        private void Awake()
        {
            _health.HealthChanged += UpdateHealthBar;
            _cam = Camera.main;
        }

        internal void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            _fill.fillAmount = currentHealth / maxHealth;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        }
    }
}
