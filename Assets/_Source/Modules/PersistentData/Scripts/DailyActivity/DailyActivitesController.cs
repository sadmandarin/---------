using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    internal class DailyActivitesController : DailyActivityBase
    {
        [SerializeField] private List<DailyActivityActionBase> _actions;
        public override void InvokeDailyActivity()
        {
            foreach (var action in _actions)
            {
                action.InvokeDailyActivityAction();
            }
        }
    }
}
