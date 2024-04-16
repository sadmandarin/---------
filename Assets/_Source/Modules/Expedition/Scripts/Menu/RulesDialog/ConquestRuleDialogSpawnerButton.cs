using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    internal class ConquestRuleDialogSpawnerButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ConquestRuleDialog _dialogPrefab;
        [SerializeField] private Canvas _canvas;

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
            var dialog = Instantiate(_dialogPrefab);
            dialog.Init(_canvas.worldCamera);
        }
    }
}
