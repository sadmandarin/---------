using System;

namespace HeroPage
{
    internal interface ITipBubble
    {
        public Action<ITipBubble> TipShown { get; set; }
        public bool IsActive { get; }
        public void Show();
        public void Hide();
    }
}
