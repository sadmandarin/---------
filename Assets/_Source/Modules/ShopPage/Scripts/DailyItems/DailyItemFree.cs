using MainPage;
using PersistentData;
using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopPage
{
    internal class DailyItemFree : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _amountToAddText;
        [SerializeField] private GameObject _claimedParent;
        [SerializeField] private FloatVariableSO _gems;
        [SerializeField] private BoolVariableSO _claimedFreeDaily;
        [SerializeField] private ItemCollectionAnimation _collectionAnimation;
        [SerializeField] private QuestsCompleter _questsCompleter;
        [SerializeField] private QuestItemDescriptionSO _quest;

        private int _gemsToAdd;

        internal void Init(int amountOfGemsToAdd)
        {
            _gemsToAdd = amountOfGemsToAdd;
            _amountToAddText.text = "x" + amountOfGemsToAdd.ToString();
            if (_claimedFreeDaily.Value == true)
                TurnOff();
            _button.onClick.AddListener(ClaimReward);
        }

        private void ClaimReward()
        {
            _gems.Value += _gemsToAdd;
            TurnOff();
            _claimedFreeDaily.Value = true;
            _collectionAnimation.PlayAnimations(true, false);
            _questsCompleter.CompleteQuest(_quest);
        }

        private void TurnOff()
        {
            _claimedParent.SetActive(true);
            _button.gameObject.SetActive(false);
        }
    }
}
