using UnityEngine;
using UnityEngine.UI;

namespace AutoBattler
{
    internal class BattleDifficultyView : MonoBehaviour
    {
        [SerializeField] private Image _skullImage, _bgImage;
        [SerializeField] private Text _title;
        [SerializeField] private GameObject _tip;
        
        internal void SetView(Sprite skullImage, Sprite bgImage, string title)
        {
            _skullImage.sprite = skullImage;
            _bgImage.sprite = bgImage;
            _title.text = title;
        }

        internal void HideTip()
        {
            _tip.SetActive(false);
        }

        internal void ShowTip()
        {
            _tip.SetActive(true);
        }

        internal void ToggleTip()
        {
            _tip.SetActive(_tip.activeInHierarchy == false);
        }
    }
}
