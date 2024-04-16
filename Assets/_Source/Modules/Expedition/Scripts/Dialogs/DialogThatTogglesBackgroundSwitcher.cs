using UnityEngine;
using YandexSDK;

namespace Expedition
{
    public class DialogThatTogglesBackgroundSwitcher : MonoBehaviour
    {
        private void OnEnable()
        {
            ApplicationActivation.Enabled = false;
            AudioListener.pause = true;
        }

        private void OnDisable()
        {
            ApplicationActivation.Enabled = true;
            AudioListener.pause = false;
        }
    }
}
