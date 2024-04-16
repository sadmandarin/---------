using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class BattlePlayerInfo : MonoBehaviour
    {
        [SerializeField] private Image _playerImage, _enemyImage;
        [SerializeField] private Text _playerText, _enemyText;
        [SerializeField] private GameObject _playerWon, _enemyWon;
        [SerializeField] private LeanPhrase _levelPhrase;
        internal void Init(int level, bool playerWon)
        {
            _playerWon.SetActive(playerWon);
            _enemyWon.SetActive(playerWon == false);
            _enemyText.text = LeanLocalization.GetTranslationText(_levelPhrase.name) + " <b>" + level + "</b>";
        }
    }
}
