using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "Variables/SavedTime")]
    public class SavedDateTimeVariableSO : ScriptableObjectVariable<JsonDateTime>
    {
        [ContextMenu(nameof(PrintTime))]
        public void PrintTime()
        {
            Debug.Log((DateTime)Value);
            Debug.Log(DateTime.MinValue.ToString());
            Debug.Log((DateTime)Value == DateTime.MinValue);
            Debug.Log(Value.value == 0);
        }

        [ContextMenu(nameof(ResetData))]
        public void ResetData()
        {
            Value = (JsonDateTime)DateTime.UtcNow;
        }

        [ContextMenu(nameof(GetToNextDay))]
        public void GetToNextDay()
        {
            Value = (JsonDateTime)DateTimeHelper.GetNextDay7AM();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
