using System.Collections.Generic;

namespace AutoBattler
{
    internal class LevelFileReader
    {
        internal EnemyFormation ReadLevelFile(LevelsHolderSO levelsHolder, int numberOfLevel)
        {
            if (numberOfLevel - 1 > levelsHolder.AllLevels.Length - 1)
                numberOfLevel = levelsHolder.AllLevels.Length - 1;
            int indexOfEnemiesFile = numberOfLevel - 1;
            var text = levelsHolder.AllLevels[indexOfEnemiesFile].text;
            string[] lines = text.Split('\n');
            int numberOfRows = lines[0].Length;
            List<string> rows = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrEmpty(line) || i == 0)
                    continue;
                if (CheckForRowWithOnlyEmptyUnits(line))
                    continue;
                rows.Add(line);
            }
            return new EnemyFormation(numberOfRows, rows.ToArray());    
        }

        private bool CheckForRowWithOnlyEmptyUnits(string row)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] != ',' && row[i] != ' ')
                    return false;
            }
            return true;
        }
    }
}
