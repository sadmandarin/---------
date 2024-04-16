using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class TowerTimer : MonoBehaviour
    {
        private float timer;
        private float[] timerDuration = {1.4f, 1.3f, 1.1f, 1f, 0.8f }; // Продолжительность таймера в секундах
        private bool timerRunning = true;

        private int level;

        [SerializeField]
        private ArrowShoot arrowShoot;

        private void OnEnable()
        {
            level = arrowShoot.spawnPos.GetComponent<Attacks>().level - 1;
        }

        void Update()
        {
            if (timerRunning)
            {
                timer += Time.deltaTime;

                if (timer >= timerDuration[level])
                {
                    timerRunning = false;

                    arrowShoot.timerOver = true;
                }
            }
        }

        public void ResetTimer()
        {
            timer = 0f;
            timerRunning = true;
        }
    }
}
