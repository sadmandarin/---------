using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    internal class ChestAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _smallGoldPile;
        [SerializeField] private GameObject _bigGoldPile;
        [SerializeField] private int _partiallyEmptyTreshold = 19200;
        [SerializeField] private int _almostFullTreshold = 14400;
        [SerializeField] private int _fullTreshold = 7200;
        private const string State = "state";

        internal void SetChestStateBasedOnTimeLeft(int timeLeft)
        {
            if (timeLeft > _partiallyEmptyTreshold)
            {
                SetChestState(PassiveChestState.Empty);
            }
            else if (timeLeft > _almostFullTreshold)
            {
                SetChestState(PassiveChestState.PartiallyEmpty);
            }
            else if (timeLeft > _fullTreshold)
            {
                SetChestState(PassiveChestState.AlmostFull);
            }
            else
            {
                SetChestState(PassiveChestState.Full);
            }
        }

        internal void SetChestState(PassiveChestState chestState)
        {
            switch (chestState)
            {
                case PassiveChestState.Empty:
                    _animator.SetInteger(State, (int)chestState);
                    _smallGoldPile.SetActive(false);
                    _bigGoldPile.SetActive(false);
                    break;
                case PassiveChestState.PartiallyEmpty:
                    _animator.SetInteger(State, (int)chestState);
                    _smallGoldPile.SetActive(false);
                    _bigGoldPile.SetActive(false);
                    break;
                case PassiveChestState.AlmostFull:
                    _animator.SetInteger(State, (int)chestState);
                    _smallGoldPile.SetActive(true);
                    _bigGoldPile.SetActive(false);
                    break;
                case PassiveChestState.Full:
                    _animator.SetInteger(State, (int)chestState);
                    _smallGoldPile.SetActive(true);
                    _bigGoldPile.SetActive(true);
                    break;
            }
        }
    }

    internal enum PassiveChestState
    {
        Empty,
        PartiallyEmpty,
        AlmostFull,
        Full
    }
}
