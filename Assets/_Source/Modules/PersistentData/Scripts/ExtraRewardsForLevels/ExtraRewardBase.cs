using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    public abstract class ExtraRewardBase : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int CommonDifference { get; private set; }
        [field: SerializeField] public int StartingNumber { get; private set; }

        [SerializeField] private LeanPhrase _descriptionWithQuantity;
        [SerializeField] private LeanPhrase _descriptionWithoutQuantity;
        [SerializeField] private LeanPhrase _basicDescription;

        public string BasicDescription => LeanLocalization.GetTranslationText(_basicDescription.name);

        public abstract void ClaimReward(int quantity);

        public string DescriptionWithQuantity(int quantity)
        {
            var translatedPhrase = LeanLocalization.GetTranslationText(_descriptionWithQuantity.name);
            return string.Format(translatedPhrase, quantity);
        }

        public virtual void GetRewardDescripton(out Sprite icon, out string description)
        {
            icon = Icon;
            description = LeanLocalization.GetTranslationText(_descriptionWithoutQuantity.name);
        }
    }
}
