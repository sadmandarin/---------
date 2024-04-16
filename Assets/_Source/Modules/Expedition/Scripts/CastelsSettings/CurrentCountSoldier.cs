using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class CurrentCountSoldier : MonoBehaviour
    {
        public GameObject[] castles;

        public Text enemyCount;

        public Text playerCount;

        private AddUnitToCastle addUnitToCastle;

        private void Start()
        {
            addUnitToCastle = GameObject.Find("LevelController").GetComponent<AddUnitToCastle>();
        }

        private void Update()
        {
            UpdateCountText();
        }

        public void UpdateCountText()
        {
            int totalEnemyCount = 0;

            int totalPlayerCount = 0;

            for (int i = 0; i < castles.Length; i++)
            {
                bool allObjectsNotInEnemyLayer = castles.All(obj => obj.gameObject.layer != LayerMask.NameToLayer("Enemy"));

                if (allObjectsNotInEnemyLayer)
                {
                    totalEnemyCount = 0;
                }

                bool allObjectsNotInPlayerLayer = castles.All(obj => obj.gameObject.layer != LayerMask.NameToLayer("Player"));

                if (allObjectsNotInPlayerLayer)
                {
                    totalEnemyCount = 0;
                }

                if (castles[i].layer == LayerMask.NameToLayer("Player"))
                {
                    totalPlayerCount += (int)(addUnitToCastle.currentSoldiers[i]);
                }
                
                else if (castles[i].layer == LayerMask.NameToLayer("Enemy"))
                {
                    totalEnemyCount += (int)(addUnitToCastle.currentSoldiers[i]);
                }

                playerCount.text = $"{totalPlayerCount}";

                enemyCount.text = $"{totalEnemyCount}";
            }
        }
    }
}
