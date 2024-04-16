using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class Attacks : MonoBehaviour
    {
        public int castleIndex;

        public int level;

        [SerializeField]
        private ButtonLevelUp levelUp;

        private void Start()
        {
            castleIndex = levelUp._castleIndex;
        }

        public void SetLevel(int level)
        {
            this.level = level;
        }

        
    }
}


