using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Expedition
{
    public class DestroySoldier : MonoBehaviour
    {
        private GameObject targetCastle;

        private GameObject startCastle;

        private AddUnitToCastle addUnitToCastle;

        private ChangeLevelSprite changeLevelSprite;

        private CurrentCountSoldier currentCountSoldier;

        private LevelController levelController;

        public delegate void ObjectDestroyedHandler(GameObject destroyedObject);

        public event ObjectDestroyedHandler ObjectDestroyed;

        private float[] castleArmor = {1.1f, 1.2f, 1.3f, 1.4f, 1.5f};

        private float[] barracksArmor = {1.3f, 1.3f, 1.4f, 1.5f, 1.6f};

        private float[] _archerArmor = {1.4f, 1.6f, 1.8f, 2f, 2.1f};

        [SerializeField]
        private GameObject objectWithMaterial;

        [SerializeField]
        private Material enemyMaterial;

        [SerializeField]
        private Material playerMaterial;

        private FightSound fight;

        private AudioSource source;

        private void Start()
        {
            addUnitToCastle = GameObject.Find("LevelController").GetComponent<AddUnitToCastle>();

            changeLevelSprite = GameObject.Find("LevelController").GetComponent<ChangeLevelSprite>();

            currentCountSoldier = GameObject.Find("Score").GetComponent<CurrentCountSoldier>();

            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

            fight = GameObject.Find("FightSound").GetComponent<FightSound>();

            source = GameObject.Find("FightSound").GetComponent<AudioSource>();

            if (gameObject.layer == LayerMask.NameToLayer("Player")) 
            {
                Renderer renderer = objectWithMaterial.GetComponent<Renderer>();

                renderer.material = playerMaterial;
            }

            else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Renderer renderer = objectWithMaterial.GetComponent<Renderer>();

                renderer.material = enemyMaterial;
            }
        }



        public void SetCastles(GameObject start, GameObject end)
        {
            startCastle = start;

            targetCastle = end;
        }


        // Start is called before the first frame update
        void OnTriggerEnter(Collider other)
        {
            var index2 = targetCastle.GetComponent<Attacks>();

            levelController.battleAnim[index2.castleIndex].SetActive(false);

            if (other.gameObject == targetCastle && other.gameObject != startCastle)
            {
                if (ObjectDestroyed != null)
                {
                    ObjectDestroyed(gameObject);
                }

                if (!source.isPlaying && other.gameObject.layer != gameObject.layer)
                {
                    source.PlayOneShot(fight.soundClip);
                }
                
                Destroy(gameObject);



                if (other.gameObject.layer == gameObject.layer && other.gameObject.tag != "Fighter")
                {
                    addUnitToCastle.currentSoldiers[index2.castleIndex]++;
                }

                else if (other.gameObject.layer != gameObject.layer && other.gameObject.tag != "Footman" || other.gameObject.tag != "Hoplites")
                {

                    Attack(other);
                }


                addUnitToCastle.UpdateUnitUI(index2.castleIndex, (int)(addUnitToCastle.currentSoldiers[index2.castleIndex]));

                
                
            }
        }

        

        void Attack(Collider other)
        {
            var index2 = targetCastle.GetComponent<Attacks>();

            levelController.battleAnim[index2.castleIndex].SetActive(true);

            if (gameObject.tag == "Footman")
            {
                if (other.gameObject.tag == "Castle")
                {
                    addUnitToCastle.currentSoldiers[index2.castleIndex] = addUnitToCastle.currentSoldiers[index2.castleIndex] - 1 / castleArmor[levelController.GetCurrentLevel(index2.castleIndex) - 1];
                }

                else if (other.gameObject.tag == "Barracks")
                {
                    addUnitToCastle.currentSoldiers[index2.castleIndex] = addUnitToCastle.currentSoldiers[index2.castleIndex] - 1 / barracksArmor[levelController.GetCurrentLevel(index2.castleIndex) - 1];
                }

                else if (other.gameObject.tag == "Archer")
                {
                    addUnitToCastle.currentSoldiers[index2.castleIndex] = addUnitToCastle.currentSoldiers[index2.castleIndex] - 1 / _archerArmor[levelController.GetCurrentLevel(index2.castleIndex) - 1];
                }
            }

            else if (gameObject.tag == "Hoplites")
            {
                if (other.gameObject.tag == "Castle")
                {
                    addUnitToCastle.currentSoldiers[index2.castleIndex] = (addUnitToCastle.currentSoldiers[index2.castleIndex] - ((1f * 1.3f) / castleArmor[levelController.GetCurrentLevel(index2.castleIndex) - 1]));
                }

                else if (other.gameObject.tag == "Barracks")
                {
                    addUnitToCastle.currentSoldiers[index2.castleIndex] = (addUnitToCastle.currentSoldiers[index2.castleIndex] - ((1f * 1.3f) / castleArmor[levelController.GetCurrentLevel(index2.castleIndex) - 1]));
                }

                else if (other.gameObject.tag == "Archer")
                {
                    addUnitToCastle.currentSoldiers[index2.castleIndex] = (addUnitToCastle.currentSoldiers[index2.castleIndex] - ((1f * 1.3f) / _archerArmor[levelController.GetCurrentLevel(index2.castleIndex) - 1]));
                }
            }
        }
    }
}
