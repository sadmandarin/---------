using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class HeroRaceAndRoleView : MonoBehaviour
    {
        [SerializeField] private Text _raceText;
        [SerializeField] private Image _raceImage;
        [SerializeField] private Text _roleText;
        [SerializeField] private Image _roleImage;

        internal void Init(string raceDescription,  Sprite raceSprite, string roleDescription, Sprite roleSprite)
        {
            _raceImage.sprite = raceSprite;
            _raceText.text = raceDescription;
            _roleImage.sprite = roleSprite;
            _roleText.text = roleDescription;
        }
    }
}
