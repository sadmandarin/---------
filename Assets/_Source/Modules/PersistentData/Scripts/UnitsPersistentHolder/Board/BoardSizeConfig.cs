using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Board/BoardSizeConfig")]
    public class BoardSizeConfig : ScriptableObject
    {
        [SerializeField] private List<SizeConfigData> _sizeConfigData;

        internal Vector2Int GetSize(int level)
        {
            List<SizeConfigData> configsUnderLevel = _sizeConfigData.Where(n => n.LevelWhenUnlocked <= level).ToList();
            return configsUnderLevel[configsUnderLevel.Count - 1].BoardSize;
        }

        public static int[,] GetActiveSlots(int width, int height)
        {
            int[,] activeSlots = new int[7, 7];
            if (width == 3 && height == 3)
                activeSlots = new int[7, 7]
                {
                    {0, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 1, 1, 1 },
                    {0, 0, 0, 0, 1, 1, 1 },
                    {0, 0, 0, 0, 1, 1, 1 },
                    {0, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0, 0, 0 }
                };
            else if (width == 5 && height == 4)
            {
                activeSlots = new int[7, 7]
                {
                    {0, 0, 0, 0, 0, 0, 0 },
                    {0, 0, 0, 1, 1, 1, 1 },
                    {0, 0, 0, 1, 1, 1, 1 },
                    {0, 0, 0, 1, 1, 1, 1 },
                    {0, 0, 0, 1, 1, 1, 1 },
                    {0, 0, 0, 1, 1, 1, 1 },
                    {0, 0, 0, 0, 0, 0, 0 }
                };
            }
            else if (width == 7 && height == 5)
            {
                activeSlots = new int[7, 7]
                {
                    {0, 0, 1, 1, 1, 1, 1 },
                    {0, 0, 1, 1, 1, 1, 1 },
                    {0, 0, 1, 1, 1, 1, 1 },
                    {0, 0, 1, 1, 1, 1, 1 },
                    {0, 0, 1, 1, 1, 1, 1 },
                    {0, 0, 1, 1, 1, 1, 1 },
                    {0, 0, 1, 1, 1, 1, 1 }
                };
            }
            else if (width == 7 && height == 6)
            {
                activeSlots = new int[7, 7]
                {
                    {0, 1, 1, 1, 1, 1, 1 },
                    {0, 1, 1, 1, 1, 1, 1 },
                    {0, 1, 1, 1, 1, 1, 1 },
                    {0, 1, 1, 1, 1, 1, 1 },
                    {0, 1, 1, 1, 1, 1, 1 },
                    {0, 1, 1, 1, 1, 1, 1 },
                    {0, 1, 1, 1, 1, 1, 1 }
                };
            }
            else if (width == 7 && height == 7)
            {
                activeSlots = new int[7, 7]
                {
                    {1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1 }
                };
            }

            return activeSlots;
        }


        [Serializable]
        internal struct SizeConfigData
        {
            public int LevelWhenUnlocked;
            public Vector2Int BoardSize;
        }
    }
}
