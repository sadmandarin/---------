using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class CombiningTroopsDialogEvent : MonoBehaviour
    {
        [SerializeField] private Image _image1;
        [SerializeField] private Image _image2;
        [SerializeField] private Sprite _level2Troop;
        [SerializeField] private Sprite _level1Troop;

        internal void ResetImages()
        {
            _image1.sprite = _level1Troop;
            _image2.sprite = _level1Troop;
        }

        internal void SwitchImages()
        {
            _image2.sprite = _level2Troop;
            _image1.sprite = _level2Troop;
        }
    }
}
