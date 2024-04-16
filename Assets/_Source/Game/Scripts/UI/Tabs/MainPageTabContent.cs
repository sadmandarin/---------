using UnityEngine;

namespace Legion
{
    public class MainPageTabContent : TabContent
    {
        [SerializeField] private GameObject _cameraForInfiniteBattle;
        public override void Hide()
        {
            _cameraForInfiniteBattle.SetActive(false);
            base.Hide();
        }

        public override void Show()
        {
            //_cameraForInfiniteBattle.SetActive(true);
            base.Show();
        }

        public override void HideCamera()
        {
            _cameraForInfiniteBattle.SetActive(false);
            base.HideCamera();
        }

        public override void ShowCamera()
        {
            _cameraForInfiniteBattle.SetActive(true);
            base.ShowCamera();
        }
    }
}