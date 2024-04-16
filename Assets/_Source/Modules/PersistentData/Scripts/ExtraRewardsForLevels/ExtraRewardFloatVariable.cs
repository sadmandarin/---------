using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(menuName = "ExtraReward/Currency")]
    public class ExtraRewardFloatVariable : ExtraRewardBase
    {
        [SerializeField] private FloatVariableSO _variable;
        [SerializeField] private VoidEventChannelSO _onVariableAdded;

        public override void ClaimReward(int quantity)
        {
            _variable.Value += quantity;
            if (_onVariableAdded != null )
            {
                _onVariableAdded.RaiseEvent();
            }
        }
    }
}
