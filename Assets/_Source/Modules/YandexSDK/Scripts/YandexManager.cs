using Agava.WebUtility;
using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using YandexSDK;

public partial class YandexManager : MonoBehaviour
{
    public static YandexManager Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this as YandexManager;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        YandexGamesSdk.CallbackLogging = true;
    }

    public Action YandexInitialized;
    public string Language { get; private set; } = "ru";

    [SerializeField] private float _timeToShowAd = 60f;

    private float _timer = 100;
    private List<PurchasedProduct> _purchasedProducts = new List<PurchasedProduct>();
    private bool _gameReadyAlreadyCalled;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        YandexInitialized?.Invoke();
        
        yield break;

#endif
        yield return YandexGamesSdk.Initialize(null);
        Billing.GetPurchasedProducts((data) => SavePurchasedProducts(data.purchasedProducts), (data) => YandexInitialized?.Invoke());
        Language = YandexGamesSdk.Environment.i18n.tld;
        if (Language == "com")
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
        else
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Russian");
        //YandexInitialized?.Invoke();
        //ShowInterstitialAd();

    }

    private void Update()
    {
        _timer += Time.deltaTime * Time.timeScale;
    }

    public bool CanShowAd()
    {
        if (_timer >= _timeToShowAd)
            return true;
        else
            return false;
    }

    public void TryShowInterstitialAd()
    {
        
//#if UNITY_EDITOR
//        return;
//#endif
        // —юда логика дл€ проверки отключени€ рекламы
        InterstitialAd.Show(InBackgroundSoundSwitcher.OnAdOpened, (data) => InBackgroundSoundSwitcher.OnAdClosed(), (data) => InBackgroundSoundSwitcher.OnAdClosed());
    }

    private void ShowRewarded(Action action)
    {
        VideoAd.Show(InBackgroundSoundSwitcher.OnAdOpened, action, InBackgroundSoundSwitcher.OnAdClosed, (data) => InBackgroundSoundSwitcher.OnAdClosed());

        Debug.Log("Showing rewarded video");
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(InBackgroundSoundSwitcher.OnAdOpened, action, InBackgroundSoundSwitcher.OnAdClosed, (data) => InBackgroundSoundSwitcher.OnAdClosed());
        EventSystem.current.SetSelectedGameObject(null);
        return;
#endif

#if !UNITY_WEBGL || UNITY_EDITOR
        action.Invoke();
#endif
    }

    public void WatchRewardedVideoWithClicker(Action action)
    {
        PreAdScreen.Instance.ShowRewarded(() => ShowRewarded(action));
    }

    public void WatchRewardedWithoutClicker(Action action)
    {
        ShowRewarded(action);
    }

    public void ShowAdWithCallbacks()
    {
        Debug.Log("Showing Ad");
        _timer = 0;
#if UNITY_EDITOR
        return;
#endif
        EventSystem.current.SetSelectedGameObject(null);
        InterstitialAd.Show(InBackgroundSoundSwitcher.OnAdOpened, (data) => InBackgroundSoundSwitcher.OnAdClosed());
    }

    

    public void PurchaseConsumable(Action action, string yandexName)
    {
#if UNITY_EDITOR
        action.Invoke();
        return;
#endif
        InBackgroundSoundSwitcher.OnAdOpened();
        Billing.PurchaseProduct(yandexName, (purchase) =>
        {
            Debug.Log($"Purchased {purchase.purchaseData.productID}");

            Billing.ConsumeProduct(purchase.purchaseData.purchaseToken, () =>
                {
                    action.Invoke();
                    InBackgroundSoundSwitcher.OnAdClosed();
                }, (data) => 
                {
                    InBackgroundSoundSwitcher.OnAdClosed();
                    Debug.LogError("Error trying to consume after purchasing " + yandexName + data);
                });

        }, (data) =>
        {
            InBackgroundSoundSwitcher.OnAdClosed();
            Debug.LogError("Error trying to purchase " + yandexName + data);
        });
    }

    public void InvokeGameReadyAPI()
    {
        if (_gameReadyAlreadyCalled) return;
#if UNITY_EDITOR
        Debug.Log("Invoking game ready API");
        return;
#endif
        YandexGamesSdk.GameReady();
        _gameReadyAlreadyCalled = true;
    }

    private void SavePurchasedProducts(PurchasedProduct[] purchasedProducts)
    {
        foreach (var purchasedProduct in purchasedProducts)
        {
            Debug.Log("Not consumed " + purchasedProduct.productID);
        }
        _purchasedProducts = purchasedProducts.ToList();
        YandexInitialized?.Invoke();
    }

    public List<PurchasedProduct> GetListOfAllItemsPurchased()
    {
        if (_purchasedProducts != null && _purchasedProducts.Count > 0)
            return _purchasedProducts.ToList();
        else
            return new List<PurchasedProduct>();
    }
        
}
