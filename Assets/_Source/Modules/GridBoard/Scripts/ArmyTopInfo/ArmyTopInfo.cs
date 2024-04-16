using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class ArmyTopInfo : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _starsImage;
        [SerializeField] private Sprite[] _starsSprites;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _upgradeArrow;

        internal void SetUp(string name, int numberOfStars)
        {
            _text.text = name;
            _starsImage.sprite = _starsSprites[numberOfStars - 1];
            _canvasGroup.alpha = 1;
        }

        internal void Move(Vector3 newPosition)
        {
            transform.position = newPosition + _offset;
        }

        internal void ToggleVisibility(bool toggle)
        {
            _canvasGroup.alpha = toggle ? 1 : 0;
        }

        internal void ToggleUpgradeArrow(bool value) => _upgradeArrow.gameObject.SetActive(value);
    }
}
