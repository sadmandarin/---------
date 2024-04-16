using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    internal class QuestsDailyActivity : DailyActivityBase
    {
        [SerializeField] private QuestsCompleter _questsCompleter;
        public override void InvokeDailyActivity()
        {
            _questsCompleter.UpdateQuests();
        }
    }
}
