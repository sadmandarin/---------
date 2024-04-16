using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class ChangeTowerRange : MonoBehaviour
    {
        [SerializeField]
        private GameObject building;

        private Attacks attack;

        [SerializeField]
        private GameObject mainBuild;

        [SerializeField]
        private GameObject image;

        private LevelController levelController;

        private void Start()
        {
            attack = mainBuild.GetComponent<Attacks>();

            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        }

        private void Update()
        {
            ChangeRange();
        }

        void ChangeRange()
        {
            var collid = building.GetComponent<SphereCollider>();

            switch (levelController.GetCurrentLevel(attack.castleIndex))
            {
                case 1:
                    collid.radius = 2.2f;

                    image.transform.localScale = new Vector3(0.9f, 0.9f, 0.6f);

                    break;

                case 2:
                    collid.radius = 2.8f;

                    image.transform.localScale = new Vector3(1.1f, 1.1f, 0.9f);

                    break;

                case 3:
                    collid.radius = 3f;

                    image.transform.localScale = new Vector3(1.2f, 1.2f, 1f);

                    break;
                case 4:
                    collid.radius = 3.2f;

                    image.transform.localScale = new Vector3(1.3f, 1.3f, 1f);

                    break;

                case 5:
                    collid.radius = 3.4f;

                    image.transform.localScale = new Vector3(1.4f, 1.4f, 0.2f); 

                    break;
            }
        }
    }
}
