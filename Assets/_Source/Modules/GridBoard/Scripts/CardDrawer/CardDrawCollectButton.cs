using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class CardDrawCollectButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private LightBallTrail _ballTrail;

        private UnitToBoardMover _unitToBoardMover;
        private CardDrawDialog _dialog;
        private string _name;
        private int _level;

        internal void Init(CardDrawDialog dialog, string name, int level, UnitToBoardMover unitToBoardMover)
        {
            _dialog = dialog;
            _name = name;
            _level = level;
            _unitToBoardMover = unitToBoardMover;
        }


        internal void CollectUnit()
        {
            _dialog.HideUIElements();
            _unitToBoardMover.MoveUnitToBoardOrBarracks(_ballTrail, _name, _level).OnComplete(() =>
            _dialog.Close());
        }

        

        private void OnEnable()
        {
            _button.onClick.AddListener(CollectUnit);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(CollectUnit);
        }
    }
}
