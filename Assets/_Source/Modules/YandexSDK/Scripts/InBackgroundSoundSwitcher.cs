using UnityEngine;

namespace YandexSDK
{
    internal static class InBackgroundSoundSwitcher
    {
        public static bool CanActivateSound => PlayerPrefs.GetInt(CanActivateSaveName) == 0 && PlayerPrefs.GetInt("Sound") == 0;

        private const string CanActivateSaveName = "CanActivateSound";
        private const string Sound = "Sound";

        public static void OnAdClosed()
        {
            AudioListener.pause = false;
            if (PlayerPrefs.GetInt(Sound) == 0)
                AudioListener.volume = 1;
            Time.timeScale = 1;
            ApplicationActivation.Enabled = true;
        }

        public static void OnAdOpened()
        {
            AudioListener.pause = true;
            AudioListener.volume = 0;
            Time.timeScale = 0;
            ApplicationActivation.Enabled = false;
        }

        public static void ToggleSound(bool activate)
        {
            PlayerPrefs.SetInt(CanActivateSaveName, activate ? 0 : 1);
            AudioListener.volume = activate == false ? 0 : PlayerPrefs.GetInt(Sound) == 0 ? 1 : 0;
        }
    }
}