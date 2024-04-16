using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public class AutoSkillButton : MonoBehaviour
    {
        [SerializeField] private Button _switchButton;
        [SerializeField] private GameObject _offView;
        [SerializeField] private ActiveSkillButton _activeSkillButton;
        [SerializeField] private TroopSpawnerCooldownUi _cooldownUi;
        [SerializeField] private TroopSpawner _troopSpawner;

        private const string PlayerPrefsSave = "AutoSkill";

        private int _autoSkill;
        private bool _isEnabled => _autoSkill == 1;

        private void OnEnable()
        {
            _autoSkill = PlayerPrefs.GetInt(PlayerPrefsSave, 0);
            SwitchView(_isEnabled);
            SwitchAutoSkillEnabled(_isEnabled);

            _switchButton.onClick.AddListener(ToggleAutoSkill);
        }

        private void OnDisable()
        {
            _switchButton.onClick.RemoveListener(ToggleAutoSkill);
        }

        private void SwitchView(bool isDoubleSpeed)
        {
            _offView.gameObject.SetActive(isDoubleSpeed == false);
        }

        private void SwitchAutoSkillEnabled(bool isDoubleSpeed)
        {
            if (isDoubleSpeed)
            {
                _activeSkillButton.ToggleAutoSkill(true);
                _cooldownUi.ToggleAutoSkill(true);
                _troopSpawner.SpawnAllTroops();
            }
            else
            {
                _activeSkillButton.ToggleAutoSkill(false);
                _cooldownUi.ToggleAutoSkill(false);
            }
        }

        private void ToggleAutoSkill()
        {
            _autoSkill = _autoSkill == 0 ? 1 : 0;
            PlayerPrefs.SetInt(PlayerPrefsSave, _autoSkill);
            SwitchAutoSkillEnabled(_isEnabled);
            SwitchView(_isEnabled);
        }
    }
}
