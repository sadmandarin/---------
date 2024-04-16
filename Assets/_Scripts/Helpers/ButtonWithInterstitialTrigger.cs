using UnityEngine;
using UnityEngine.UI;

public class ButtonWithInterstitialTrigger : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(TryShowInter);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryShowInter);
    }

    private void TryShowInter()
    {
        PreAdScreen.Instance.ShowAdClicker();
    }
}
