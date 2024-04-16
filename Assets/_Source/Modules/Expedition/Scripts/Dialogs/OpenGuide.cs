using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Expedition
{
    public class OpenGuide : MonoBehaviour
    {
        [SerializeField]
        private Guide guide;

        private void OnEnable()
        {
            guide.SetObject();
        }
    }
}
