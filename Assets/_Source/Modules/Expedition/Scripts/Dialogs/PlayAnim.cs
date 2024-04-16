using PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class PlayAnim : MonoBehaviour
    {
        [SerializeField]
        private GameObject lightning;

        private bool isAwakening = false;

        [SerializeField]
        private LevelVariable levelVariable;

        [SerializeField]
        private ConquestLevelsCollection conquestLevels;

        [SerializeField]
        private GameObject hand;

        private void Update()
        {
            if (isAwakening == false && conquestLevels.GetLevelData(20).Finished && levelVariable.Value >= 80)
            {
                lightning.SetActive(true);

                hand.SetActive(conquestLevels.GetLevelData(21).Finished == false);
            }
        }
    }
}
