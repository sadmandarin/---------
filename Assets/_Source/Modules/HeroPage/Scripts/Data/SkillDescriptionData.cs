using System;
using UnityEngine;

namespace HeroPage
{
    [Serializable]
    internal class SkillDescriptionData
    {
        internal SkillDescriptionItemViewSO View => _view;
        internal float GetValue(int level) => Values[Mathf.Clamp(level, 0, Values.Length - 1)];

        [SerializeField] private SkillDescriptionItemViewSO _view;
        [SerializeField] private float[] Values;
    }
}
