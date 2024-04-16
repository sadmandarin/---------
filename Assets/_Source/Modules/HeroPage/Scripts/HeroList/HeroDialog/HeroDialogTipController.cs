using System.Collections.Generic;
using UnityEngine;

namespace HeroPage
{
    internal class HeroDialogTipController : MonoBehaviour
    {
        [SerializeField] private List<BasicDescriptionBubble> _basicDescriptionBubble; 

        private List<ITipBubble> _tipBubbles = new List<ITipBubble>();

        internal void AddTipBubble(ITipBubble tipBubble)
        {
            _tipBubbles.Add(tipBubble);
            tipBubble.TipShown += HandleTipBubbleShown;
        }

        internal void RemoveTipBubble(ITipBubble tipBubble)
        {
            tipBubble.TipShown -= HandleTipBubbleShown;
            _tipBubbles.Remove(tipBubble);
        }

        internal void HandleTipBubbleShown(ITipBubble shownTip)
        {
            foreach (var tip in _tipBubbles)
            {
                if (tip != shownTip)
                    tip.Hide();
            }
        }

        private void OnEnable()
        {
            foreach (var tip in _basicDescriptionBubble)
            {
                AddTipBubble(tip);
            }
        }

        private void OnDisable()
        {
            foreach (var tip in _basicDescriptionBubble)
            {
                RemoveTipBubble(tip);
            }
        }
    }
}
