using System;
using UnityEngine;

namespace GridBoard
{
    internal class MovedToBarracksTrigger : MonoBehaviour
    {
        internal Action<bool> SomeoneInsideBarracksStateChanged;

        internal void InvokeSomeoneInsideBarracksStateChanged(bool toggle)
        {
            SomeoneInsideBarracksStateChanged?.Invoke(toggle);
        }
    }
}
