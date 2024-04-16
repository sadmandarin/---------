using PersistentData;
using Quests;
using UnityEngine;

namespace MainPage
{
    internal class ChestItemPurchases : MonoBehaviour
    {
        [SerializeField] private FloatVariableSO _coins;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private QuestItemDescriptionSO _quest;
        [SerializeField] private Canvas _canvas;
        private ChestVariableSO _chestVariable;
        private int _amountForPurchase;

        internal void Init(ChestVariableSO chestVariable, int amountForPurchase)
        {
            _chestVariable = chestVariable;
            _amountForPurchase = amountForPurchase;
        }

        internal void UpdatePrice(int amountForPurchase)
        {
            _amountForPurchase = amountForPurchase;  
        }

        internal void OpenChest()
        {
            _chestVariable.OpenChest();
            _questsCompleter.CompleteQuest(_quest);
            var rewardDialog = Instantiate(_chestVariable.ChestDialogPrefab);
            rewardDialog.GetComponent<ChestRewardDialog>().Init();
        }

        internal void PurchaseAndOpenChest()
        {
            if(_chestVariable.UsesGemsForOpening)
            {
                _gems.Value -= _amountForPurchase;
            }
            else
            {
                _coins.Value -= _amountForPurchase;
            }
            OpenChest();
        }
    }
}
