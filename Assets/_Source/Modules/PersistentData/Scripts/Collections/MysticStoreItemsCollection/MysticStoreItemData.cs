using System;

namespace PersistentData
{
    [Serializable]
    public struct MysticStoreItemData
    {
        public string GUID;
        public bool AlreadyClaimed;

        public MysticStoreItemData(string guid, bool alreadyClaimed)
        {
            GUID = guid;
            AlreadyClaimed = alreadyClaimed;
        }

        public MysticStoreItemData ClaimItem()
        {
            return new MysticStoreItemData { GUID = this.GUID, AlreadyClaimed = true };
        }
    }
}
