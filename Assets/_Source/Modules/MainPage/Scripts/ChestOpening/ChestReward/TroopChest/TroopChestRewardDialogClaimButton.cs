using PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class TroopChestRewardDialogClaimButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UnitAdder _unitAdder;
        [SerializeField] private GameObject _dialogParent;

        private string _name;
        private int _level;

        internal void Init(string name, int level)
        {
            _name = name;
            _level = level;
            _unitAdder.AddUnit(_name, _level);
            
            _button.onClick.AddListener(ClaimTroop);
        }

        private void ClaimTroop()
        {
            Destroy(_dialogParent);
        }
    }
}
