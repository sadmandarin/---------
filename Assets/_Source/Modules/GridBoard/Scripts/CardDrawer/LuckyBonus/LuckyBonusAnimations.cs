using DG.Tweening;
using UnityEngine;

namespace GridBoard
{
    internal class LuckyBonusAnimations : MonoBehaviour
    {
        [SerializeField] private GameObject _luckyBonusPanel;
        [SerializeField] private GameObject _luckyBonusMultiplier;

        internal Sequence AnimateLuckyBonus()
        {
            return DOTween.Sequence().AppendCallback(TurnOnLuckyObjects)
                              .Append(_luckyBonusPanel.transform.DOScale(1, 0.25f));
        }

        private void TurnOnLuckyObjects()
        {
            _luckyBonusMultiplier.SetActive(true);
            _luckyBonusPanel.SetActive(true);
        }
    }
}
