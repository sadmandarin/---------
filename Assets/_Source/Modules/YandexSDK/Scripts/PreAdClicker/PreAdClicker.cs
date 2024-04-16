using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PersistentData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PreAdClicker : MonoBehaviour
{
    [SerializeField] private ActiveLanguageSwitcher scoreCounter;
    [SerializeField] private float spawnDelay;
    [SerializeField] private ClickObject clickObjectPrefab;
    [SerializeField] private Transform tutorialHand;
    [SerializeField] private GameObject tutorialText;
    [SerializeField] private FloatVariableSO _playerMoney;
    [SerializeField] private float _moneyForOneCoin = 100f;
    [SerializeField] private Vector3 _handOffset;
    [SerializeField] private Transform _positionForCoinToMove;
    [SerializeField] private FloatingText _floatingText;

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private Tween handTween;
    
    private int score;

    private bool spawning;
    
    public static UnityEvent ScoreAdded { get; } = new UnityEvent();

    

    private void AddScore(Transform coinTransform)
    {
        DOTween.Sequence().Append(coinTransform.DOScale(0.1f, 0.1f).SetRelative(true))
                          .Append(coinTransform.DOMove(_positionForCoinToMove.position, 0.3f))
                          .Join(coinTransform.DOScale(0.5f, 0.3f))
                          .AppendCallback(() => Destroy(coinTransform.gameObject));
        FloatingText floatingText = Instantiate(_floatingText, coinTransform.position, _floatingText.transform.rotation, transform);
        floatingText.SetText("+" + _moneyForOneCoin);
        score++;
        scoreCounter.UpdateValue(score * (int)_moneyForOneCoin);
        _playerMoney.Value += (_moneyForOneCoin);
        ScoreAdded.Invoke();

        if (tutorialText.activeSelf || tutorialHand != null)
        {
            tutorialText.SetActive(false);
            tutorialHand.gameObject.SetActive(false);
            handTween.Kill();
        }
    }

    private void ResetField()
    {
        score = 0;
        scoreCounter.UpdateValue(score);

        foreach (var go in instantiatedObjects)
        {
            if (go)
                Destroy(go);
        }
        instantiatedObjects.Clear();
    }

    public void StartField()
    {
        spawning = true;
        scoreCounter.UpdateValue(score);
        
        StartCoroutine(SpawnObjects());

        if (instantiatedObjects.Count >= 0)
        {
            var hand = Instantiate(tutorialHand.gameObject, instantiatedObjects[0].transform);
            hand.SetActive(true);
            hand.transform.localPosition = Vector3.zero + _handOffset;
            handTween = hand.transform.DOScale(1.25f, .5f).SetLoops(-1, LoopType.Yoyo);
        }
        tutorialText.gameObject.SetActive(true);
        
    }

    public void StopField()
    {
        spawning = false;
        handTween.Kill();
        ResetField();
    }

    private IEnumerator SpawnObjects()
    {
        while (spawning)
        {
            ClickObject button = Instantiate(clickObjectPrefab, 
                new Vector3(Random.Range(100, Screen.width - 100), 100, 0), 
                Quaternion.identity, 
                transform);

            button.OnClicked += AddScore;
            
            instantiatedObjects.Add(button.gameObject);
            
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
