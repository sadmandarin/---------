using UnityEngine;
using UnityEngine.UI;

public class MoneyTimer : MonoBehaviour 
{
    [SerializeField] protected int _timeToClaimReward = 120;
    [SerializeField] protected Image _fill;
    [SerializeField] protected Text _remainingTimeText;
    [SerializeField] private Button _claimButton;
    [SerializeField] private Animator _animator;

    protected bool _unlocked;
    protected float _timer = 0;

    private const string Idle = "btn_normal";
    private const string Shake = "watchAdShake";

    protected virtual void Awake()
    {
        ResetTimer();
    }

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(ClaimReward);
    }

    private void OnDisable()
    {
        _claimButton.onClick.RemoveListener(ClaimReward);
    }

    private void Update()
    {
        if (_unlocked) return;

        _timer += Time.deltaTime;
        SetFillAndText();
        if (_timer > _timeToClaimReward)
        {
            UnlockTimer();
        }
    }

    protected virtual void SetFillAndText()
    {
        _fill.fillAmount = 1 - _timer / _timeToClaimReward;
        _remainingTimeText.text = FormatFloatToTime(_timeToClaimReward - _timer);
    }

    private void UnlockTimer()
    {
        _claimButton.gameObject.SetActive(true);
        _fill.fillAmount = 0;
        _unlocked = true;
        _remainingTimeText.gameObject.SetActive(false);
        _animator.Play(Shake);
    }

    protected virtual string FormatFloatToTime(float secondsLeft)
    {
        return secondsLeft.ToString();
    }

    protected virtual void ClaimReward()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        _claimButton.gameObject.SetActive(false);
        _timer = 0;
        _unlocked = false;
        _remainingTimeText.gameObject.SetActive(true);
        _animator.Play(Idle);
    }
}
