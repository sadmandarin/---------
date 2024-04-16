using UnityEngine;

namespace Legion
{
    internal class MainMenu : MenuBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _infiniteBattleCamera;
        public override void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _infiniteBattleCamera.SetActive(false);
        }

        public override void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _infiniteBattleCamera.SetActive(true);
        }
    }
}
