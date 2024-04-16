using PersistentData;
using System;
using UnityEngine;

namespace MainPage
{
    internal class OfflineRewardsController : MonoBehaviour
    {
        [SerializeField] private Camera _canvasCamera;
        [SerializeField] private OfflineRewardsDialog _dialogPrefab;
        [SerializeField] private ChestPresser _chestPresser;
        [SerializeField] private ChestAnimator _chestAnimator;
        [SerializeField] private OfflineRewardsValuesCalculator _valuesCalculator;
        [SerializeField] private OfflineTimer _offlineTimer;
        [SerializeField] private float _coinsPerHour = 1000;
        [SerializeField] private float _gemsPerHour = 15;
        [SerializeField] private FloatVariableSO _coins;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private ItemCollectionAnimation _collectionAnimation;
        [SerializeField] private MainPageObjectsHider _mainPageHider;

        private int _coinsReward, _gemsReward;

        private void Awake()
        {
            _chestPresser.ChestPressed += HandleChestPressed;
            
        }

        private void Start()
        {
            _chestAnimator.SetChestStateBasedOnTimeLeft(_offlineTimer.CalculateTimeLeft());
        }

        private void HandleChestPressed()
        {
            SpawnDialog();
            _mainPageHider.ToggleVisibility();
        }

        private void SpawnDialog()
        {
            var dialog = Instantiate(_dialogPrefab);
            dialog.Init(_canvasCamera);
            dialog.DialogClosed += HandleDialogClosed;
            dialog.ClaimButtonPressed += HandleClaimButtonPressed;
            _valuesCalculator.CalculateRewards(_offlineTimer.CalculateTimeLeft(), _coinsPerHour, _gemsPerHour, out int coins, out int gems);
            _gemsReward = gems;
            _coinsReward = coins;
            dialog.SetValues(coins, gems);
        }

        private void HandleClaimButtonPressed()
        {
            _coins.Value += _coinsReward;
            _gems.Value += _gemsReward;
            _offlineTimer.ResetStartingTime();
            _collectionAnimation.PlayAnimations(_gemsReward > 0, _coinsReward > 0);
            _mainPageHider.ToggleVisibility();
            _chestAnimator.SetChestStateBasedOnTimeLeft(_offlineTimer.CalculateTimeLeft());
        }

        private void HandleDialogClosed()
        {
            _mainPageHider.ToggleVisibility();
            _offlineTimer.UpdateTime();
            _chestAnimator.SetChestStateBasedOnTimeLeft(_offlineTimer.CalculateTimeLeft());
        }
    }
}
