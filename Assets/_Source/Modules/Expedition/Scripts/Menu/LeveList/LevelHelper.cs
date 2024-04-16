namespace Expedition
{
    internal static class LevelHelper
    {
        internal static int NormalizeLevelForDisplay(int level)
        {
            while (level > 20)
                level -= 20;
            return level;
        }
    }
}
