using UnityEngine;

namespace Legion
{
    public class HeroPageTabContent : TabContent
    {
        [SerializeField] private HeroPage.TroopListPage _troopListPage;
        [SerializeField] private HeroPage.HeroListPage _heroListPage;

        public override void Hide()
        {
            base.Hide();
        }

        public override void Show()
        {
            base.Show();
            _troopListPage.SetUp();
            _heroListPage.UpdateListItems();
        }
    }
}