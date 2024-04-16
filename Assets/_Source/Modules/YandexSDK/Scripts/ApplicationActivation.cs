using Agava.WebUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YandexSDK
{
    public class ApplicationActivation : MonoBehaviour
    {
        // Enabled �����, ����� �� ����� ������� ��������� ������������� ���������� ���� 
        // (����� ���� ����� ������ � ������ �� ��������� ���� ��� timescale ������ 1)
        public static bool Enabled = true;
        private static bool _instanceCreated;

        private void Awake()
        {
#if !UNITY_EDITOR
            // ����������� ��, ����� ������� ��� ����
            if (_instanceCreated)
            {
                Destroy(this);
                return;
            }
            _instanceCreated = true;
            DontDestroyOnLoad(gameObject);
            // ������������� �� ������� � ��������� ��������� ���������� ����
            WebApplication.InBackgroundChangeEvent += OnGoBackground;
            StartCoroutine(PollBackgroundChange());
#endif
        }

        private IEnumerator PollBackgroundChange()
        {
            while (true)
            {
                // ������� �� �����, ������ ���� ��� ���������� �������� �� ������
                yield return new WaitForSeconds(1.5f);
                // InBackground ���������� document.hidden
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
            // ��������� ��� ������ - ������ ������ ��������������� ���������/���������� ����� �� ���� ����
            if (InBackgroundSoundSwitcher.CanActivateSound)
            {
                AudioListener.pause = obj;
                AudioListener.volume = obj ? 0 : 1;
            }
            
        }
    }
}
