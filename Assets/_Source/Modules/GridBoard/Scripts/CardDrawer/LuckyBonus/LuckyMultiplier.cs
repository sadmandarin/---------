using PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace GridBoard
{
    internal class LuckyMultiplier : MonoBehaviour
    {
        [SerializeField] private Text _multiplierText;
        [SerializeField] private LuckyConfigSO _configSO;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private ParticleSystem _critEffect;

        internal void AddLuckyBonusAndSetUp(int levelOfUnit)
        {
            int multiplier = _configSO.GetLuckyMultiplier();
            _configSO.AddLuckyBonus(multiplier, levelOfUnit);
            if (multiplier == 1)
            {
                _canvasGroup.alpha = 0;
            }
            else
            {
                _canvasGroup.alpha = 1;
                _multiplierText.text = "X" + multiplier;
                _critEffect.Play();
            }
        }
    }
}
