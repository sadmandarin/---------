using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPage
{
    internal class SkillsTroopPage : MonoBehaviour
    {
        [SerializeField] private Image _skillIcon;
        [SerializeField] private Text _skillTitle;
        [SerializeField] private Text _skillDescription;
        [SerializeField] private Transform _parentToSpawnPrefab;
        [SerializeField] private SkillDescriptionItem _descriptionItemPrefab;

        private List<GameObject> _skillDescriptionItems = new List<GameObject>();

        internal void SetUp(Sprite skillIcon, string skillTitle, string skillDescription)
        {
            _skillIcon.sprite = skillIcon;
            _skillTitle.text = skillTitle;
            _skillDescription.text = skillDescription;
        }

        internal void ChangeSkillDescription(TroopSkillView skillView, int level)
        {
            ClearPreviousSkillDescriptions();

            foreach (var skillDescription in skillView.DescriptionItems)
            {
                var description = Instantiate(_descriptionItemPrefab, _parentToSpawnPrefab);
                description.Set(skillDescription.View.Icon, skillDescription.View.Title, skillDescription.GetValue(level - 1));
                _skillDescriptionItems.Add(description.gameObject);
            }
        }

        private void ClearPreviousSkillDescriptions()
        {
            foreach (var item in _skillDescriptionItems)
            {
                Destroy(item);
            }
            _skillDescriptionItems.Clear();
        }
    }
}
