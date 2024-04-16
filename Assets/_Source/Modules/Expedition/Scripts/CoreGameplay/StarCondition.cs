using PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Expedition
{
    public class StarCondition : MonoBehaviour
    {
        private int level;

        private bool[] starsBool = new bool[3];

        [SerializeField]
        private GameObject[] stars;

        [SerializeField]
        private ConquestLevelsCollection conquestLevelsCollection;


        [SerializeField] private ConquestLevelRequirementsConfig requirementsConfig;

        [SerializeField]
        private CompletionRewards completionRewards;

        private LevelController levelController;

        private GameManager gameManager;

        void OnEnable()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            level = gameManager.level;

            completionRewards.SetUp(level);

            StarConditionForLevel();

            StarOn();

            FinishLevel();
        }

        // Update is called once per frame
        void StarConditionForLevel()
        {
            var levelRequirement = requirementsConfig.Requirements[level - 1];

            for (int i = 0; i < starsBool.Length; i++)
            {
                var requirement = levelRequirement.Requirements[i].Requirement;

                var requirementValues = levelRequirement.Requirements[i].RequirementValues;

                bool requirementStar = requirement.IsRequirementFullfilled(requirementValues, levelController);

                starsBool[i] = requirementStar;
            }
        }

        void StarOn()
        {

            for (int i = 0;i < stars.Length;i++)
            {
                if (starsBool[i])
                {
                    stars[i].SetActive(true);
                }
            }
        }

        void FinishLevel()
        {
            int star = 0;

            for (int i = 0; i < 3; i++)
            {
                if (stars[i].activeSelf)
                {
                    star++;
                }
            }

            conquestLevelsCollection.FinishLevel(level, star);
        }
    }
}
