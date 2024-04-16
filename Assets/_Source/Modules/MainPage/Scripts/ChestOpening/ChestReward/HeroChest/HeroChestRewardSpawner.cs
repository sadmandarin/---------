using Lean.Localization;
using System.Linq;
using UnitsData;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage
{
    internal class HeroChestRewardSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parentToSpawn;
        [SerializeField] private HeroPresentationsList _heroesList;
        [SerializeField] private GameObject _explosionFx;
        [SerializeField] private ParticleSystem _shiningFx;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Text _heroNameText;
        [SerializeField] private GameObject _newHeroText, _alreadyHasHero;

        private string _heroName;

        internal void Init(string heroName)
        {
            _heroName = heroName;
        }

        internal void SpawnHero(bool isHeroNew)
        {
            _explosionFx.SetActive(true);
            _shiningFx.Play();
            _audioSource.Play();
            _heroNameText.text = LeanLocalization.GetTranslationText(_heroName + "Title");
            _newHeroText.SetActive(isHeroNew);
            _alreadyHasHero.SetActive(isHeroNew == false);
            Instantiate(_heroesList.Heroes.First(n => n.HeroName.ToString() == _heroName).PrefabForPresentionInUi, _parentToSpawn);
        }
    }
}
