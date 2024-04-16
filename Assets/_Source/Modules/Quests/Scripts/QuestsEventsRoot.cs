using System;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(menuName = "Quests/EventsRoot")]
    public class QuestsEventsRoot : ScriptableObject
    {
        public Action OnDialogOpened;
        public Action OnRewardsClaimed;
        public Action OnDialogClosed;

        public void InvokeDialogClosed()
        {
            OnDialogClosed?.Invoke();
        }

        public void InvokeDialogOpened()
        {
            OnDialogOpened?.Invoke();
        }
    }
}
