using System;
using UnityEngine;

namespace GridBoard
{
    internal class BattleSlotVisualizer : MonoBehaviour
    {
        [SerializeField] private GameObject _crossUnderUnit;
        [SerializeField] private ParticleSystem _availableForUpgradeEffect;
        [SerializeField] private ParticleSystem _upgradedEffect;
        [SerializeField] private Renderer _slotRenderer;
        [SerializeField] private Material _lockedMaterial;
        [SerializeField] private Material _unlockedMaterial;
        [SerializeField] private Material _selectedMaterial;
        [SerializeField] private AudioClip _upgradeSound;

        internal void ToggleAvailableForUpgradeEffect(bool value)
        {
            _availableForUpgradeEffect.gameObject.SetActive(value);
        }

        internal void PlayUpgradeEffect()
        {
            if (_upgradedEffect != null)
            {
                _upgradedEffect.Play();
                // PlayUpgradeSound
            }
        }

        internal void ToggleCross(bool value)
        {
            _crossUnderUnit.SetActive(value);
        }

        internal void Select()
        {
            _slotRenderer.material = _selectedMaterial;
        }

        internal void Unselect()
        {
            _slotRenderer.material = _unlockedMaterial;
        }

        internal void SetLockedMaterial()
        {
            _slotRenderer.material = _lockedMaterial;
        }
    }
}
