using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    public class TipContent : MonoBehaviour
    {
        [SerializeField]
        private Text content;

        void FillContentTip(Text text)
        {
            content  = text;
        }
    }
}
