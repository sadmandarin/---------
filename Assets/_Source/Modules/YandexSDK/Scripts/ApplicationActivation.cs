using Agava.WebUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YandexSDK
{
    public class ApplicationActivation : MonoBehaviour
    {
        // Enabled нужен, чтобы на время рекламы выключать прослушивание активности игры 
        // (иначе если игрок выйдет и войдет то включится звук или timescale станет 1)
        public static bool Enabled = true;
        private static bool _instanceCreated;

        private void Awake()
        {
#if !UNITY_EDITOR
            // Гарантируем то, чтобы инстанс был один
            if (_instanceCreated)
            {
                Destroy(this);
                return;
            }
            _instanceCreated = true;
            DontDestroyOnLoad(gameObject);
            // Подписываемся на событие и проверяем поллингом активность окна
            WebApplication.InBackgroundChangeEvent += OnGoBackground;
            StartCoroutine(PollBackgroundChange());
#endif
        }

        private IEnumerator PollBackgroundChange()
        {
            while (true)
            {
                // Частота не важна, лагать даже при регулярной проверке не должно
                yield return new WaitForSeconds(1.5f);
                // InBackground возвращает document.hidden
                OnGoBackground(WebApplication.InBackground);
            }
        }

        private void OnGoBackground(bool obj)
        {
            Debug.Log($"{nameof(ApplicationActivation)} {nameof(OnGoBackground)} called. is background == {obj}");
            if (Enabled == false)
            {
                return;
            }

            Time.timeScale = obj ? 0 : 1;
            // Следующие две строки - лучший способ контроллировать включение/выключение аудио во всей игре
            if (InBackgroundSoundSwitcher.CanActivateSound)
            {
                AudioListener.pause = obj;
                AudioListener.volume = obj ? 0 : 1;
            }
            
        }
    }
}
