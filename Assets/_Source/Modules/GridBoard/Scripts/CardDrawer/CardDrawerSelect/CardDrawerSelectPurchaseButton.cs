using PersistentData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class CardDrawerSelectPurchaseButton : MonoBehaviour
    {
        internal Action OnDialogSpawned;

        [SerializeField] private Button _button;
        [SerializeField] private FloatVariableSO _coins;
        [SerializeField] private Text _text;
        [SerializeField] private Sprite _activeBg, _inactiveBg;
        [SerializeField] private GameObject _notEnoughCoinsDialog;
        [SerializeField] private CardDrawDialog _cardDrawDialog;
        [SerializeField] private CardDrawerConfig _config;
        
        private bool _hasEnoughMoney;
        private CardDrawerOdds _odds;
        private UnitToBoardMover _unitToBoardMover;
        private Transform _parentToSpawnDialog;

        internal void UpdatePrice(int newPrice, CardDrawerOdds odds)
        {
            _odds = odds;
            _text.text = newPrice.ToString();
            _hasEnoughMoney = newPrice <= _coins.Value;
            _button.image.sprite = _hasEnoughMoney ? _activeBg : _inactiveBg;
        }

        internal void Init(UnitToBoardMover unitToBoardMover, Transform parentToSpawnDialog)
        {
            _unitToBoardMover = unitToBoardMover;
            _parentToSpawnDialog = parentToSpawnDialog;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SpawnDialog);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SpawnDialog);
        }

        private void SpawnDialog()
        {
            OnDialogSpawned?.Invoke();

            if (_hasEnoughMoney)
            {
                SpawnNewCardDialog();
            }
            else
            {
                SpawnNotEnoughMoneyDialog();
            }
        }

        private void SpawnNewCardDialog()
        {
            _coins.Value -= _odds.Price;

            var dialog = Instantiate(_cardDrawDialog,_parentToSpawnDialog);
            var troopName = _config.GetRandomUnit(_odds);
            var level = _odds.LevelOfUnit;
            dialog.Init(troopName, SpawnNewCardDialog, _unitToBoardMover, _odds.Price, level);
        }

        private void SpawnNotEnoughMoneyDialog()
        {
            var dialog = Instantiate(_notEnoughCoinsDialog);
        }
    }
}
