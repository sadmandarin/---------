using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    public class BattleReportFactionItem : MonoBehaviour
    {
        [SerializeField] private Text _troopDamageDealtText;
        [SerializeField] private Text _troopDamageTakenText;
        [SerializeField] private Text _troopDamageBlockedText;
        [SerializeField] private Text _troopHealedText;
        [SerializeField] private Image _heroImage;
        [SerializeField] private Text _heroNameText;
        [SerializeField] private GameObject _noHeroParent;
        [SerializeField] private GameObject _hasHeroParent;
        [SerializeField] private Text _heroDamageDealtText;
        [SerializeField] private Text _heroDamageTakenText;
        [SerializeField] private Text _heroDamageBlockedText;
        [SerializeField] private Text _heroHealedText;


        internal void InitHeroStats(bool hasHero, float damageDealt = 0 , float damageTaken = 0, float damageBlocked = 0, float healed = 0)
        {
            if (hasHero == false)
            {
                _hasHeroParent.SetActive(false);
                _noHeroParent.SetActive(true);
            }    

            _heroDamageDealtText.text = damageDealt.ToString("0.");
            _heroDamageTakenText.text = damageTaken.ToString("0.");
            _heroDamageBlockedText.text = damageBlocked.ToString("0.");
            _heroHealedText.text = healed.ToString("0.");
        }

        internal void InitHeroPictureAndName(Sprite heroIcon, string name)
        {
            _heroImage.sprite = heroIcon;
            _heroNameText.text = Lean.Localization.LeanLocalization.GetTranslationText(name);
        }

        internal void InitFactionStats(float damageDealt, float damageTaken, float damageBlocked, float healed)
        {
            _troopDamageBlockedText.text = damageBlocked.ToString("0.");
            _troopDamageDealtText.text = damageDealt.ToString("0.");
            _troopDamageTakenText.text = damageTaken.ToString("0.");
            _troopHealedText.text = healed.ToString("0.");
        }
    }
}
