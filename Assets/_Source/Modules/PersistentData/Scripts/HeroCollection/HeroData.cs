using System;

namespace PersistentData
{
    [Serializable]
    public struct HeroData
    {
        public string Name;
        public bool Unlocked;
        public int CollectedShards;
        public int Level;
        public bool Selected;
        public bool IsNew;

        public HeroData(string name, bool unlocked, int collectedShards, int level, bool selected, bool isNew)
        {
            Name = name;
            Unlocked = unlocked;
            CollectedShards = collectedShards;
            Level = level;
            Selected = selected;
            IsNew = isNew;
        }

        public HeroData(string name)
        {
            Name = name;
            Unlocked = false;
            CollectedShards = 0;
            Level = 1;
            Selected = false;
            IsNew = false;
        }

        public HeroData Select()
        {
            return new HeroData(this.Name, this.Unlocked, this.CollectedShards, this.Level, true, false);
        }

        public HeroData Unselect()
        {
            return new HeroData(this.Name, this.Unlocked, this.CollectedShards, this.Level, false, false);
        }

        public HeroData Unlock()
        {
            return new HeroData(this.Name, true, this.CollectedShards, this.Level, this.Selected, true);
        }

        public HeroData AddShard()
        {
            return new HeroData(this.Name, this.Unlocked, this.CollectedShards + 1, this.Level, this.Selected, this.IsNew);
        }

        public HeroData RemoveShards(int amountToRemove)
        {
            return new HeroData(this.Name, this.Unlocked, this.CollectedShards - amountToRemove, this.Level, this.Selected, this.IsNew);
        }

        public HeroData AddLevel()
        {
            return new HeroData(this.Name, this.Unlocked, this.CollectedShards, this.Level + 1, this.Selected, this.IsNew);
        }
    }
}
