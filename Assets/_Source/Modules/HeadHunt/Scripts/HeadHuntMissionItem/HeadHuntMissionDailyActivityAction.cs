using PersistentData;
using UnityEngine;

namespace HeadHunt
{
    internal class HeadHuntMissionDailyActivityAction : DailyActivityActionBase
    {
        [SerializeField] private LevelVariable _missionLevelVariable;
        [SerializeField] private IntVariableSO _timesRemainingVariable;

        public override void InvokeDailyActivityAction()
        {
            _missionLevelVariable.Value = 1;
            _timesRemainingVariable.Value = 2;
        }
    }


}
