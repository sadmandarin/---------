using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class FillCount : MonoBehaviour
    {
        private AddUnitToCastle addUnit;

        private LevelController levelController;

        private Image image;

        [SerializeField]
        private Attacks attacks;

        private void OnEnable()
        {
            addUnit = GameObject.Find("LevelController").GetComponent<AddUnitToCastle>();

            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

            image = GetComponent<Image>();
        }

        private void Update()
        {
            Fill();
        }

        private void Fill()
        {
            for (int i = 0; i < addUnit.currentSoldiers.Length; i++) 
            {
                float fillCount = addUnit.currentSoldiers[i] / levelController.maxSoldierForLevel[levelController.castleLevel[i] - 1];

                if (attacks.castleIndex == i)
                {
                    if (fillCount < 1)
                    {
                        image.fillAmount = fillCount;
                    }

                    else
                    {
                        image.fillAmount = 1;
                    }
                }
                
            }
        }
    }
}
