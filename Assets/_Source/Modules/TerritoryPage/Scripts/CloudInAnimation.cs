using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TerritoryPage
{
    internal class CloudInAnimation : MonoBehaviour
    {
        [SerializeField] private Image[] _clouds;
        [SerializeField] private Animator _cloudAnimator;
        [SerializeField] private float _timeToAnimate = 1f;

        private const string CloudsIn = "c_in";

        private void OnEnable()
        {
            _cloudAnimator.Play(CloudsIn);
            foreach (var cloud in _clouds)
            {
                cloud.DOFade(1, _timeToAnimate);
            }
        }
    }
}
