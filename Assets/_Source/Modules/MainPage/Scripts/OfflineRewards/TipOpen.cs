using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    public class TipOpen : MonoBehaviour
    {
        [SerializeField]
        private GameObject tipDialog;

        private bool isOpen = false;

        public void OnClick()
        {
            if (!isOpen)
            {
                OpenTip();
            }
            else
            {
                CloseTip();
            }
        }

        private void OpenTip()
        {
            tipDialog.SetActive(true);

            isOpen = true;
        }

        private void CloseTip()
        {
            tipDialog.SetActive(false);

            isOpen = false;
        }
    }
}
