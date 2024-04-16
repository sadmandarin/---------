using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class GameWin : MonoBehaviour
    {
        [SerializeField]
        private Text opponentScore;

        [SerializeField]
        private GameObject winPanel;

        
        private LevelController levelController;

        private AddUnitToCastle addUnit;

        [SerializeField]
        private GameObject levels;

        [SerializeField]
        private GameObject timer;

        List<GameObject> objectsOfTypeFootman = new List<GameObject>();

        List<GameObject> objectsOfTypeHoplites = new List<GameObject>();
        
        private PauseAndGameEndDialog endDialog;

        private bool flag = false;

        public GameObject menu;

        private GameManager gameManager;


        private int level;
        private void SetUpData()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            level = gameManager.level;

            endDialog.SetupData(level);
        }


        private void OnEnable()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

            addUnit = GameObject.Find("LevelController").GetComponent<AddUnitToCastle>();
        }

        private void Update()
        {
            int currOppScore = int.Parse(opponentScore.text);

            if (levelController.AllEnemyCastleCaptured() && flag == false)
            {
                StartCoroutine(Win());

                flag = true;
            }
        }

        IEnumerator Win()
        {
            yield return new WaitForSeconds(1.7f);

            menu = Instantiate(winPanel);

            endDialog = menu.GetComponent<PauseAndGameEndDialog>();

            SetUpData();
            //flag = true;

            for (int i = 0; i < levelController.anim.Length; i++)
            {
                levelController.anim[i].SetActive(false);

                levelController.battleAnim[i].SetActive(false);

                levelController.blueAnim[i].SetActive(false);

                levelController.redAnim[i].SetActive(false);

                levels.SetActive(false);

                timer.SetActive(false);

                GameObject[] objectsArray = GameObject.FindGameObjectsWithTag("Footman");

                GameObject[] objectsArray1 = GameObject.FindGameObjectsWithTag("Hoplites");

                addUnit.AddUnitToCastlePrefab[i].SetActive(false);

                objectsOfTypeFootman.AddRange(objectsArray);

                foreach (GameObject obj in objectsOfTypeFootman)
                {
                    Destroy(obj);
                }

                // Очистим список после уничтожения объектов, если это необходимо
                objectsOfTypeFootman.Clear();

                objectsOfTypeHoplites.AddRange(objectsArray1);

                foreach (GameObject obj in objectsOfTypeFootman)
                {
                    Destroy(obj);
                }

                objectsOfTypeHoplites.Clear();
            }
        }
    }
}

