using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class LevelListItem : MonoBehaviour
    {
        [SerializeField] private GameObject _lockedParent;
        [SerializeField] private GameObject _currentParent;
        [SerializeField] private GameObject _finishedParent;
        [SerializeField] private GameObject _extraRewardParent;
        [SerializeField] private Text _levelText;
        [SerializeField] private Image _extraRewardItem;

        private bool _finished;

        internal void SetUp(LevelListItemState state, int level)
        {
            Color gray = new Color(0,0,0, 0.2f);
            //gray.a = 0.8f;
            switch (state)
            {
                case LevelListItemState.Locked:
                    _lockedParent.SetActive(true);
                    _currentParent.SetActive(false);
                    _finishedParent.SetActive(false);
                    _levelText.color = gray;
                    transform.localScale = Vector3.one;
                    break;
                case LevelListItemState.Current:
                    _lockedParent.SetActive(false);
                    _currentParent.SetActive(true);
                    _finishedParent.SetActive(false);
                    _levelText.color = Color.white;
                    transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    break;
                case LevelListItemState.Finished:
                    _lockedParent.SetActive(false);
                    _currentParent.SetActive(false);
                    _finishedParent.SetActive(true);
                    _levelText.color = gray;
                    _finished = true;
                    transform.localScale = Vector3.one;
                    break;
            }
            _levelText.text = level.ToString();
        }

        internal void AddReward(Sprite icon)
        {
            if (_finished)
            {
                _extraRewardParent.SetActive(false);
                return;
            }    
                
            _extraRewardParent.SetActive(true);
            _extraRewardItem.sprite = icon;
        }
    }

    internal enum LevelListItemState
    {
        Locked,
        Current,
        Finished
    }
}
