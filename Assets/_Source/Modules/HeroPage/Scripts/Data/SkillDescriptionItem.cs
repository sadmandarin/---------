using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class SkillDescriptionItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _title;
        [SerializeField] private Text _valueText;

        internal void Set(Sprite icon, string title, float value)
        {
            _icon.sprite = icon;
            _title.text = title;
            _valueText.text = value.ToString();
        }
    }
}
