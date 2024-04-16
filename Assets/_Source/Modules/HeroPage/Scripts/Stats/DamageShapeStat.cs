using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class DamageShapeStat : MonoBehaviour
    {
        [SerializeField] private Image _icon; 
        [SerializeField] private Text _text; 
        [SerializeField] private Sprite[] _damageShapeSprites;
        
        internal void Set(TypeOfDamageShape damageShape)
        {
            _text.text = LeanLocalization.GetTranslationText(damageShape.ToString());
            _icon.sprite = _damageShapeSprites[(int)damageShape];
        }
    }
}