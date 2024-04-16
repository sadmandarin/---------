using UnityEngine;
using UnityEngine.UI;

namespace Legion
{
    internal class ButtonWithMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip _music;
        [SerializeField] private Button _button;
        [SerializeField] private float _delay = 0;

        private void OnEnable()
        {
            _button.onClick.AddListener(PlaySound);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PlaySound);
        }

        private void PlaySound()
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlayMusic(_music, _delay);
            else
                Debug.LogError("SoundManager is not instantiated");
        }
    }
}
