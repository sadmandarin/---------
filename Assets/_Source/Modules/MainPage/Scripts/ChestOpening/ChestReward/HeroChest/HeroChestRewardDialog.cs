using DG.Tweening;
using PersistentData;
using System;
using System.Linq;
using UnityEngine;

namespace MainPage
{
    internal class HeroChestRewardDialog : ChestRewardDialog
    {
        [SerializeField] private ChestRewardCardInitializer _initializer;
        [SerializeField] private HeroChestRewardLoot _loot;
        [SerializeField] private HeroCollection _heroCollection;
        [SerializeField] private ChestCardAnimation _cardAnimation;
        [SerializeField] private HeroChestRewardSpawner _spawner;

        private bool _isSpawnedHeroNew;

        public override void SetUp()
        {
            _loot.GetRandomHero(out Sprite troopIcon, out string troopName, out int rarity, out string defaultName);
            _initializer.InitCard(troopIcon, troopName, rarity);
            _spawner.Init(defaultName);
            _isSpawnedHeroNew = _heroCollection.HasHeroInCollection(defaultName);
            _heroCollection.AddHero(defaultName);
        }

        private void OnEnable()
        {
            _cardAnimation.OnAnimationFinished += HandleCardAnimationFinished;
        }

        private void OnDisable()
        {
            _cardAnimation.OnAnimationFinished -= HandleCardAnimationFinished;
        }

        private void HandleCardAnimationFinished()
        {
            DOTween.Sequence().Append(_cardAnimation.transform.DOScale(0, 0.5f))
                              .AppendCallback(() => _spawner.SpawnHero(_isSpawnedHeroNew));
        }
    }
}
