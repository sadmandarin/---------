using PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class ChestAdderTest : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ChestVariableSO _variableSO;

        private void OnEnable()
        {
            _button.onClick.AddListener(AddChest);
        }

        private void AddChest()
        {
            _variableSO.AddChest();
        }
    }
}
