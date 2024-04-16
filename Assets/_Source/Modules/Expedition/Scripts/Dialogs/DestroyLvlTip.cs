using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class DestroyLvlTip : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        private GameObject tipObject;

        [SerializeField]
        private GameObject shield;

        [SerializeField]
        private LevelUpGuide lvlguide;

        public void CloseTip()
        {
            lvlguide.state++;

            lvlguide.DisableObject();
        }
    }
}
