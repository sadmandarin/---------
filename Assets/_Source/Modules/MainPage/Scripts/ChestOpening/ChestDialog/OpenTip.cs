using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    public class OpenTip : MonoBehaviour
    {
        [SerializeField]
        private GameObject tipDialog;

        private bool isOpen = false;

        public void OnClick()
        {
            if (!isOpen)
            {
                Tip();
            }
            else
            {
                CloseTip();
            }
        }

        public void Tip()
        {
            tipDialog.SetActive(true);

            isOpen = true;
        }

        public void CloseTip()
        {
            tipDialog.SetActive(false);

            isOpen = false;
        }
    }
}
