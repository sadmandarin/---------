using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Expedition
{
    public class ArrowShoot : MonoBehaviour
    {
        [SerializeField]
        private GameObject arrowPrefab;

        private bool hasSpawn = false;

        [SerializeField]
        internal GameObject spawnPos;

        private List<GameObject> objectsInside = new List<GameObject>();

        [SerializeField]
        private GameObject ring;

        private Transform soldierTransform;

        private SphereCollider sphereCollider;

        bool hasSoldierEntered = false;

        GameObject arrow;

        private List<GameObject> arrowList = new List<GameObject>();

        internal bool timerOver = true;

        [SerializeField]
        private TowerTimer timer;

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Footman" || other.gameObject.tag == "Hoplites")
            { 
                if (other.gameObject.layer != spawnPos.layer)
                {
                    objectsInside.Add(other.gameObject);

                    if (soldierTransform == null)
                    {
                        soldierTransform = objectsInside.First(item => item != null).transform;
                    }

                    

                    ring.SetActive(true);

                    hasSoldierEntered = true;
                }

                if (hasSoldierEntered && arrow == null && soldierTransform != null && timerOver)
                {
                    SpawnInterval();

                    timerOver = false;

                    timer.ResetTimer();
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (objectsInside.Contains(other.gameObject))
            {
                objectsInside.Remove(other.gameObject);
            }
        }

        private void Update()
        {

            

            objectsInside.RemoveAll(item => item == null);

            if (soldierTransform == null && objectsInside.Count > 0)
            {
                soldierTransform = objectsInside[0].transform;

            }

            if (soldierTransform != null)
            {
                float distance = Vector3.Distance(gameObject.transform.position, soldierTransform.position);

                sphereCollider = GetComponent<SphereCollider>();

                //if (distance - 1.2 >= sphereCollider.radius)
                //{
                //    CancelInvoke();

                //    hasSoldierEntered = false;

                //    ring.SetActive(false);
                //}
            }

            //else
            //{
            //    ring.SetActive(false);

            //    CancelInvoke();
            //}

            if (arrow == null)
            {
                arrowList.Clear();
            }



            if (arrow != null && arrow.transform.position.y <= -142.5f)
            {
                Destroy(arrow);

                arrowList.Clear();
            }

            if (objectsInside.TrueForAll(item => item == null))
            {
                hasSoldierEntered = false;

                objectsInside.Clear();

                Destroy(arrow);

                arrowList.Clear();

                CancelInvoke();

                ring.SetActive(false);
            }

            

            
        }

        void SpawnInterval()
        {
            var spawn = spawnPos.transform.position + new Vector3(0, 2, 0);

            arrow = Instantiate(arrowPrefab, spawn, spawnPos.transform.rotation);

            arrowList.Add(arrow);

            arrow.transform.LookAt(soldierTransform);

            arrow.transform.DOJump(soldierTransform.position, 2f, 1, 0.5f);
            
        }

    }
}

