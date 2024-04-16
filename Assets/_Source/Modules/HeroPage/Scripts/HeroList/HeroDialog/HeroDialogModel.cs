using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroPage
{
    public class HeroDialogModel : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private const string Skill = "Skill";

        private void OnEnable()
        {
            _animator.SetTrigger(Skill);
        }
    }
}
