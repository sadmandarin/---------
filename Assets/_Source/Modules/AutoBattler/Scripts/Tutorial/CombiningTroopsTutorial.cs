using PersistentData;
using UnityEngine;

namespace AutoBattler
{
    internal class CombiningTroopsTutorial : MonoBehaviour
    {
        [SerializeField] private LevelVariable _levelVariable;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CombiningTroopsDialog _combiningTroopsDialog;

        internal void SpawnTutorial()
        {
            var dialog = Instantiate(_combiningTroopsDialog);
            dialog.InitDialog(_canvas.worldCamera);
        }
    }
}
