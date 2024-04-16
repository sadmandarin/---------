using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Expedition
{
    public class GameLost : MonoBehaviour
    {
        [SerializeField]
        private Text timerText;
        
        [SerializeField]
        private Text playerScore;

        [SerializeField]
        private GameObject prefabLostGame;

        [SerializeField]
        private GameObject timerObject;

        private LevelController levelController;

        
        private PauseAndGameEndDialog endDialog;

        [SerializeField]
        private GameObject levels;

        private GameManager gameManager;

        private int level;

        private bool isSpawned = false;

        public GameObject endMenu;

        private void Start()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        }

        private void SetUpData()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            level = gameManager.level;

            endDialog.SetupData(level);
        }

        void Update()
        {
            int currScorePlayer = int.Parse(playerScore.text);

            if ((levelController.AllPlayerCastleCaptured() || timerText.text == "00:00") && isSpawned == false)
            {
                endMenu = Instantiate(prefabLostGame);
                endDialog = endMenu.GetComponent<PauseAndGameEndDialog>();
                if (endDialog != null )
                {
                    SetUpData();
                }
                isSpawned = true;

                Time.timeScale = 0f; 
            }
        }
    }
}
