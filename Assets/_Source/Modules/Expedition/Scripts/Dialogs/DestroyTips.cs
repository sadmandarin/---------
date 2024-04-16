using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class DestroyTips : MonoBehaviour
    {
        [SerializeField]
        private GameObject tipObject;

        [SerializeField]
        private GameObject shield;

        [SerializeField]
        private Guide guide;

        public void CloseTip()
        {
            Time.timeScale = 1f;

            guide._state++;

            shield.SetActive(true);

            guide.DisableObject();
            
        }
    }
}
