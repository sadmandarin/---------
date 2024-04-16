using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeadHunt
{
    internal class HeadHuntMissionDailyActivity : DailyActivityBase
    {
        [SerializeField] private LevelVariable _missionLevelVariable;
        [SerializeField] private IntVariableSO _timesRemainingVariable;

        internal void Init(LevelVariable levelVariable, IntVariableSO timesRemainingVariable)
        {
            _missionLevelVariable = levelVariable;
            _timesRemainingVariable = timesRemainingVariable;
        }

        public override void InvokeDailyActivity()
        {
            _missionLevelVariable.Value = 1;
            _timesRemainingVariable.Value = 2;
        }

        [ContextMenu(nameof(Test))]
        public void Test()
        {
            InvokeDailyActivity();
        }
    }


}
