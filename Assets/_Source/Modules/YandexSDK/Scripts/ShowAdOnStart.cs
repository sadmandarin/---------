using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YandexSDK
{
    public class ShowAdOnStart : MonoBehaviour
    {
        private void Start()
        {
            if (PreAdScreen.Instance != null) 
                PreAdScreen.Instance.ShowAdClicker();
        }
    }
}
