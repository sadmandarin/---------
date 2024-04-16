using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class BarracksTroopCell : MonoBehaviour, IBarracksTroopCell
    {
        public int Level { get => _level; private set => _level = value; }
        public string Name { get => _name; private set => _name = value; }

        internal Action<IBarracksTroopCell> CellPressed;

        [SerializeField] private Image _bg;
        [SerializeField] private Sprite[] _raritiesForBg;
        [SerializeField] private Image _icon;
        [SerializeField] private Text _troopLevel;
        [SerializeField] private Image _upgradeAvailableImage;
        [SerializeField] private Button _button;

        private bool _followMouse;
        private int _level;
        private string _name;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ClickHandler);
        }

        private void OnDisable()
        {
            _button?.onClick.RemoveListener(ClickHandler);
        }

        private void ClickHandler()
        {
            CellPressed?.Invoke(this);
        }

        internal void Init(Sprite iconTroop, int rarity, int troopLevel, string name)
        {
            _level = troopLevel;
            _name = name;
            _bg.sprite = _raritiesForBg[rarity];
            _icon.sprite = iconTroop;
            _troopLevel.text = troopLevel.ToString();
        }

        internal void StartFollowingMouse()
        {
            _followMouse = true;
        }

        internal void StopFollowingMouse()
        {
            _followMouse = false;
        }

        private void Update()
        {
            if (_followMouse)
            {
                transform.position = Input.mousePosition;
            }
        }

        
    }
}
