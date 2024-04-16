using UnityEngine;

namespace HeroPage
{
    internal class HeroOriginDialogSpawner : MonoBehaviour
    {
        [SerializeField] private HeroOriginDialog _originDialog;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _parent;

        private GameObject _heroCamera;

        internal void Init(GameObject heroCamera)
        {
            _heroCamera = heroCamera;
        }

        internal void ShowDialog()
        {
            var dialog = Instantiate(_originDialog);

            dialog.InitCamera(_canvas.worldCamera, _heroCamera);
            dialog.OnMovingEvent += () => Destroy(_parent.gameObject);
        }
    }
}
