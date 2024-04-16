using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroPage
{
    internal static class CustomTextFormatter
    {
        internal static string ToBoldAndBlack(string text)
        {
            return "<b><color=black>" + text + "</color></b>";
        }
        internal static string ToGrey(string text)
        {
            return "<color=grey>" + text + "</color>";
        }
    }
}
