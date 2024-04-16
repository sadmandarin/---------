using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{ 
    public class AddUnitToCastle : MonoBehaviour
    {
        public GameObject[] AddUnitToCastlePrefab;

        public GameObject[] decreaseUnit;

        public Text[] soldierText;

        [SerializeField]
        public float[] currentSoldiers;

        private float[] _castleAndBarrackRecoverSpeed = { 0.6f, 0.8f, 1f, 1, 1.2f };

        public float archerRecoverSpeed = 0;

        private float elapsedTime = 0f;

        private float spawnInterwal = 1f;

        public LevelController levelController;

        private GameManager gameManager;

        private void Awake()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        }

        void OnEnable()
        {

            currentSoldiers = new float[soldierText.Length];

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            for (int i = 0; i < currentSoldiers.Length; i++)
            {
                currentSoldiers[i] = float.Parse(soldierText[i].text);
            }
        }

        public int[] GetCurrentSoldiers(Text text)
        {
            for (int i = 0; i < soldierText.Length; i++)
            {
                currentSoldiers[i] = float.Parse(soldierText[i].text);
            }
            return null;
        }

        

        // Update is called once per frame
        void Update()
        {
            ShowLevelUpArrow();

            HideLevelUpArrow();

            if (elapsedTime >= spawnInterwal)
            {
                AddSoldier();

                RemoveSoldier();

                elapsedTime = 0f;
            }
            elapsedTime += Time.deltaTime;

            for (int i = 0;i < currentSoldiers.Length;i++)
            {
                if (currentSoldiers[i] <= 0f)
                {
                    currentSoldiers[i] = 0;
                }
            }

        }

        

        private void AddSoldier()
        {
            for (int i = 0; i < soldierText.Length; i++)
            {
                int currLvl = levelController.GetCurrentLevel(i);


                if (currentSoldiers[i] < levelController.maxSoldierForLevel[currLvl - 1])
                {
                    if ((gameManager.level == 1 || gameManager.level == 2) && gameManager.castle[i].layer == LayerMask.NameToLayer("Enemy") && AddUnitToCastlePrefab[i].tag != "Archer")
                    {
                        currentSoldiers[i] = currentSoldiers[i] + 0.5f;
                    }

                    else
                    {
                        CoeffAddSoldier(i, currLvl - 1);

                        
                    }

                    UpdateUnitUI(i, (int)(currentSoldiers[i]));

                }
            }
        }

        void ShowLevelUpArrow()
        {
            for (int i = 0; i < soldierText.Length; i++)
            {
                int currLvl = levelController.GetCurrentLevel(i);
                if (!levelController.anim[i].activeSelf)
                {
                    if ((currentSoldiers[i] >= levelController.soldierNextLvl[currLvl - 1]) && currLvl != 5 && gameManager.castle[i].layer == LayerMask.NameToLayer("Player"))
                    {
                        levelController.canLevelUp[i].SetActive(true);
                    }
                }
            }
            
        }

        void HideLevelUpArrow()
        {
            for(int i = 0;i < soldierText.Length;i++)
            {
                int currlvl = levelController.GetCurrentLevel(i);

                if (levelController.canLevelUp[i].activeSelf)
                {
                    if (currentSoldiers[i] < levelController.soldierNextLvl[currlvl - 1])
                    {
                        levelController.canLevelUp[i].SetActive(false);
                    }
                }
            }
        }

        void CoeffAddSoldier(int index, int currentLevel)
        {
            if (AddUnitToCastlePrefab[index].tag == "Castle" || AddUnitToCastlePrefab[index].tag == "Barracks")
            {
                currentSoldiers[index] = currentSoldiers[index] + _castleAndBarrackRecoverSpeed[currentLevel]; 
            }

            else if (AddUnitToCastlePrefab[index].tag == "Archer")
            {
                currentSoldiers[index] = currentSoldiers[index];
            }
        }

        private void RemoveSoldier()
        {
            for (int i = 0; i < soldierText.Length; i++)
            {
                Text harmText = decreaseUnit[i].GetComponent<Text>();
                int currLvl = levelController.GetCurrentLevel(i);

                if ((int)currentSoldiers[i] > levelController.maxSoldierForLevel[currLvl - 1])
                {
                    currentSoldiers[i]--;

                    decreaseUnit[i].SetActive(true);

                    harmText.text = string.Format("-1");

                    UpdateUnitUI(i, (int)(currentSoldiers[i]));

                    //decreaseUnit[i].GetComponent<DecreaseUnit>().SetDecrease(1);
                }
            }
        }

        public void UpdateUnitUI(int index, int currentSoldier)
        {
            soldierText[index].text = "" + currentSoldier;
        }
    }
}
