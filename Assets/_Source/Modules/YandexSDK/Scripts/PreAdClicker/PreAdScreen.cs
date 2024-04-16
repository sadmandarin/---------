using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using YandexSDK;
using static YandexManager;
using GameAnalyticsSDK;

[RequireComponent(typeof(CanvasGroup))]
public class PreAdScreen : MonoBehaviour
{
    [SerializeField] private ActiveLanguageSwitcher timer;
    [SerializeField] private int adDelaySec;
    [SerializeField] private PreAdClicker clicker;

    public static PreAdScreen Instance;

    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        if(!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        canvasGroup = GetComponent<CanvasGroup>();

        //if (GameAnalytics.Initialized == false)
        //    GameAnalytics.Initialize();
    }

    // Самый главный кусок который тебе нужен, вызывай вместо интеров,
    // он сам проверяет от Eiko, меняй на агаву если надо,
    // не забудь таймер на 90 секунд между рекламами
    public void ShowAdClicker()
    {
        //#if UNITY_EDITOR
        //        Debug.Log("Showing Ad");
        //        return;
        //#endif
        if (YandexManager.Instance.CanShowAd())
        {
            if (GameAnalytics.IsRemoteConfigsReady())
            {
                string clicker = GameAnalytics.GetRemoteConfigsValueAsString("Clicker", "0");
                if (clicker == "0")
                {
                    YandexManager.Instance.ShowAdWithCallbacks();
                    return;
                }
                else if (clicker == "1")
                    StartCoroutine(AdTimer());
            }
            else
            {
                YandexManager.Instance.ShowAdWithCallbacks();
            }
        }
    }

    private IEnumerator AdTimer()
    {
        AnimatedShow();
        clicker.StartField();
        InBackgroundSoundSwitcher.ToggleSound(false);
        for (int i = adDelaySec; i > 0; i--)
        {
            timer.UpdateValue(i);
            if (i == 1)
            {
                yield return new WaitForSeconds(0.5f);
                YandexManager.Instance.ShowAdWithCallbacks();
                //yield return new WaitForSeconds(0.5f);
                continue;
            }    

            yield return new WaitForSeconds(1);
        }
//#if UNITY_EDITOR
        InBackgroundSoundSwitcher.ToggleSound(true);
//#endif
        AnimatedHide();
        clicker.StopField();
    }

    public void ShowRewarded(Action action)
    {
        if (GameAnalytics.IsRemoteConfigsReady())
        {
            string clicker = GameAnalytics.GetRemoteConfigsValueAsString("Clicker", "0");
            if (clicker == "0")
            {
                action.Invoke();
                return;
            }
            else if (clicker == "1")
                StartCoroutine(AdTimerWithCallback(action));
        }
        else
        {
            action.Invoke();
        }
    }

    private IEnumerator AdTimerWithCallback(Action callback)
    {
        AnimatedShow();
        clicker.StartField();
        InBackgroundSoundSwitcher.ToggleSound(false);
        for (int i = adDelaySec; i > 0; i--)
        {
            timer.UpdateValue(i);
            if (i == 1)
            {
                yield return new WaitForSeconds(0.5f);
                callback.Invoke();
                //yield return new WaitForSeconds(0.5f);
                continue;
            }
            yield return new WaitForSeconds(1);
        }
//#if UNITY_EDITOR
        InBackgroundSoundSwitcher.ToggleSound(true);
//#endif
        AnimatedHide();
        clicker.StopField();
    }

    private void AnimatedShow()
    {
        canvasGroup.DOFade(1, 0.25f)
            .OnComplete(() =>
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
    }

    private void AnimatedHide()
    {
        canvasGroup
            .DOFade(0, 0.25f)
            .OnComplete(() =>
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
    }
}
