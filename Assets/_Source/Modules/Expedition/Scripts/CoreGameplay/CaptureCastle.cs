using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class CaptureCastle : MonoBehaviour
    {
        private LevelController levelController;

        private ChangeLevelSprite changeLevelSprite;

        private Attacks index2;

        private AddUnitToCastle addUnitToCastle;

        [SerializeField]
        private int targetLevel;

        private void Start()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

            changeLevelSprite = GameObject.Find("LevelController").GetComponent<ChangeLevelSprite>();

            addUnitToCastle = GameObject.Find("LevelController").GetComponent<AddUnitToCastle>();

            index2 = GetComponent<Attacks>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (addUnitToCastle.currentSoldiers[index2.castleIndex] <= 0f)
            {
                 ChangeLayer(other);
            }
        }

        void ChangeLayer(Collider other)
        {
            if (levelController.anim[index2.castleIndex].activeSelf == true)
            {
                levelController.anim[index2.castleIndex].SetActive(false);
            }

            if (gameObject.layer == other.gameObject.layer)
            {
                return;
            }

            else if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                gameObject.layer = LayerMask.NameToLayer("Enemy");

                changeLevelSprite.gameObjects[index2.castleIndex].layer = LayerMask.NameToLayer("Enemy");

                levelController.castleLevel[index2.castleIndex] = 1;

                levelController.capturePlayerCastle++;

                changeLevelSprite.CampChange(index2.castleIndex, 0);

                
            }

            else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                gameObject.layer = LayerMask.NameToLayer("Player");

                changeLevelSprite.gameObjects[index2.castleIndex].layer = LayerMask.NameToLayer("Player");

                if (levelController.castleLevel[index2.castleIndex] >= targetLevel)
                {
                    levelController.countCaptureCastleOfLevel++;
                }

                if (levelController.castleLevel[index2.castleIndex] == targetLevel)
                {
                    levelController.countCaptureCastleOfCurrentLevel++;
                }

                levelController.castleLevel[index2.castleIndex] = 1;

                changeLevelSprite.CampChange(index2.castleIndex, 0);

                
            }

            else if (gameObject.layer == LayerMask.NameToLayer("Neutral"))
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    gameObject.layer = LayerMask.NameToLayer("Player");

                    changeLevelSprite.gameObjects[index2.castleIndex].layer = LayerMask.NameToLayer("Player");

                    if (levelController.castleLevel[index2.castleIndex] >= targetLevel)
                    {
                        levelController.countCaptureCastleOfLevel++;
                    }

                    if (levelController.castleLevel[index2.castleIndex] == targetLevel)
                    {
                        levelController.countCaptureCastleOfCurrentLevel++;
                    }

                    levelController.castleLevel[index2.castleIndex] = 1;

                    changeLevelSprite.CampChange(index2.castleIndex, 0);
                }

                else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    gameObject.layer = LayerMask.NameToLayer("Enemy");

                    changeLevelSprite.gameObjects[index2.castleIndex].layer = LayerMask.NameToLayer("Enemy");

                    levelController.castleLevel[index2.castleIndex] = 1;

                    changeLevelSprite.CampChange(index2.castleIndex, 0);
                }


            }
        }
    }
}
