using JetBrains.Annotations;
using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class LevelController : MonoBehaviour
    {
        public Text[] levelText;

        public GameObject[] canLevelUp;

        public GameObject[] anim;

        public GameObject[] blueAnim;

        public GameObject[] redAnim;

        public GameObject[] battleAnim;

        public GameObject[] campChange;

        public GameObject[] buildingLevel;

        public List<int> castleLevel = new List<int>();

        public int[] maxSoldierForLevel = new int[5];

        public int[] soldierNextLvl = new int[5];

        public AddUnitToCastle addUnitToCastle;

        public ChangeLevelSprite changeLevelSprite;

        public int timerSecond = 0;

        private int[] LevelUpCost = new int[] { 5, 10, 15, 20 };

        public int countCaptureCastleOfLevel = 0;

        public int countCaptureCastleOfCurrentLevel = 0;

        public int capturePlayerCastle = 0;

        public GameManager gameManager;

        private void OnEnable()
        {
            addUnitToCastle = GameObject.Find("LevelController").GetComponent<AddUnitToCastle>();

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            maxSoldierForLevel[0] = 10;
            maxSoldierForLevel[1] = 20;
            maxSoldierForLevel[2] = 30;
            maxSoldierForLevel[3] = 40;
            maxSoldierForLevel[4] = 50;

            for (int i = 0; i < buildingLevel.Length; i++)
            {
                var index = buildingLevel[i].GetComponent<Attacks>();

                castleLevel.Add(index.level);
            }

            for (int i = 0; i < levelText.Length; i++)
            {
                SetSoldier(i, castleLevel[i] - 1);
            }
        }

        void Start()
        {
            for (int i = 0; i < buildingLevel.Length; i++)
            {
                var index = buildingLevel[i].GetComponent<Attacks>();

                castleLevel.Add(index.level);
            }

            changeLevelSprite = GameObject.Find("LevelController").GetComponent<ChangeLevelSprite>();

            soldierNextLvl[0] = 5;
            soldierNextLvl[1] = 10;
            soldierNextLvl[2] = 15;
            soldierNextLvl[3] = 20;
            soldierNextLvl[4] = 30;

            for (int i = 0; i < buildingLevel.Length; i++)
            {
                UpdateLevelText(i, GetCurrentLevel(i) - 1);
            }
        }

        public void SetSoldier(int i, int currentLvl)
        {

            addUnitToCastle.soldierText[i].text = "" + maxSoldierForLevel[currentLvl];

        }

        public int GetCurrentLevel(int index)
        {
            for (int i = 0; i < castleLevel.Count; i++)
            {
                if (i == index)
                {
                    return castleLevel[i];
                }
            }
            return -1;
        }

        public void IncreaseLevel(int index)
        {
            //int currentLevel = castlesLevel[index];

            if (!anim[index].activeSelf)
            {
                if (castleLevel[index] < 5 && addUnitToCastle.currentSoldiers[index] >= soldierNextLvl[GetCurrentLevel(index) - 1])
                {
                    Text harmText = addUnitToCastle.decreaseUnit[index].GetComponent<Text>();

                    var indexBuild = buildingLevel[index].GetComponent<Attacks>();

                    addUnitToCastle.currentSoldiers[index] = addUnitToCastle.currentSoldiers[index] - LevelUpCost[castleLevel[index] - 1];

                    addUnitToCastle.decreaseUnit[index].SetActive(true);

                    harmText.text = string.Format("-" + LevelUpCost[castleLevel[index] - 1]);

                    campChange[index].SetActive(false);

                    castleLevel[index]++;

                    indexBuild.SetLevel(castleLevel[index]);

                    canLevelUp[index].SetActive(false);



                    addUnitToCastle.UpdateUnitUI(index, (int)(addUnitToCastle.currentSoldiers[index]));

                    changeLevelSprite.ChangeSprites(index, castleLevel[index]);
                }
            }
        }

        public void UpdateLevelText(int index, int currentLevel)
        {
            if (currentLevel + 1 == 5)
            {
                MaxLevelPhrase maxLevelPhrase = levelText[index].GetComponent<MaxLevelPhrase>();

                maxLevelPhrase.UpdateText();
            }

            else
            {
                LevelPhrase leanPhrase = levelText[index].GetComponent<LevelPhrase>();

                leanPhrase.UpdateText(currentLevel + 1);
            }
        }

        public bool AllEnemyCastleCaptured()
        {
            int flag = 0;

            foreach (var item in gameManager.castle)
            {
                if (item.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    flag++;
                }
            }

            return flag == 0;
        }

        public bool AllPlayerCastleCaptured()
        {
            int flag = 0;

            foreach(var item in gameManager.castle)
            {
                if (item.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    flag++;
                }
            }

            return flag == 0;
        }
    

        public bool AllCastleCatpured()
        {
            int flag = 0;

            foreach (var item in gameManager.castle)
            {
                if (item.gameObject.layer == LayerMask.NameToLayer("Enemy") || item.gameObject.layer == LayerMask.NameToLayer("Neutral"))
                {
                    flag++;
                }
            }

            return flag == 0;
        }

    }
}