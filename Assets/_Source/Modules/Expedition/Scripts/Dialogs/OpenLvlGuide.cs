using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class OpenLvlGuide : MonoBehaviour
    {
        [SerializeField]
        private LevelUpGuide guide;

        private void OnEnable()
        {
            guide.SetObject();
        }
    }
}
