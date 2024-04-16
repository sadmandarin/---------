using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

namespace Expedition
{
    public class Timer : MonoBehaviour
    {
        public float countTime = 60f;

        public Text countDownText;

        public int second = 0;

        private LevelController levelController;

        private void Start()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

            StartCoroutine(DoCheck());
        }

        private void Update()
        {
            if (countTime > 0)
            {
                countTime -= Time.deltaTime;

                UpdateTimerUi();
            }

            else if (countTime == 0)
            {
                countTime = 0;
            }

            
        }

        void UpdateTimerUi()
        {
            float minutes = Mathf.FloorToInt(countTime / 60f);

            float seconds = Mathf.FloorToInt(countTime % 60f);

            countDownText.text = String.Format("{0:00}:{1:00}", minutes, seconds);

        }

        IEnumerator DoCheck()
        {
            while (countTime > 0)
            {
                second++;

                levelController.timerSecond = second;

                yield return new WaitForSeconds(1f);
            }
        }
    }
}