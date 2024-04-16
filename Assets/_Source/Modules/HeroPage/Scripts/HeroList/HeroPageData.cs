namespace HeroPage
{
    internal struct HeroPageData
    {
        public HeroViewSO HeroView;
        public int Level;
        public int Shards;
        public bool IsUnlocked;
        public bool IsSelected;
        public bool IsNew;

        public HeroPageData(HeroViewSO heroView, int level, int shards, bool isUnlocked, bool isSelected, bool isNew)
        {
            HeroView = heroView;
            Level = level;
            Shards = shards;
            IsUnlocked = isUnlocked;
            IsSelected = isSelected;
            IsNew = isNew;
        }
    }
}
