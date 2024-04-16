using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class SpawnGroup : MonoBehaviour
    {
        [SerializeField]
        private GameObject left;

        [SerializeField]
        private GameObject right;

        [SerializeField]
        private GameObject center;

        [SerializeField]
        private GameObject[] soldierprefab;

        private SpavnSoldier spawnsoldier;

        private GameObject startCastles, endCastle;

        private GameObject[] soldier = new GameObject[3];

        private bool isFootman = true;

        private void OnEnable()
        {
            spawnsoldier = GameObject.Find("GameManager").GetComponent<SpavnSoldier>();
        }

        public void FillGroup()
        {
            for (int j = 0; j < 3; j++)
            {
                if (gameObject.CompareTag("Castle") || gameObject.CompareTag("Archer"))
                {
                    if (j == 0)
                    {
                        soldier[0] = Instantiate(soldierprefab[0], left.transform.position, Quaternion.identity);

                        soldier[0].transform.parent = gameObject.transform;

                        soldier[0].layer = gameObject.layer;
                    }

                    else if (j == 1)
                    {
                        soldier[1] = Instantiate(soldierprefab[0], center.transform.position, Quaternion.identity);

                        soldier[1].transform.parent = gameObject.transform;

                        soldier[1].layer = gameObject.layer;
                    }

                    else if (j == 2)
                    {
                        soldier[2] = Instantiate(soldierprefab[0], right.transform.position, Quaternion.identity);

                        soldier[2].transform.parent = gameObject.transform;

                        soldier[2].layer = gameObject.layer;
                    }
                }

                else
                {
                    if (j == 0)
                    {
                        soldier[0] = Instantiate(soldierprefab[1], left.transform.position, Quaternion.identity);

                        soldier[0].transform.parent = gameObject.transform;

                        soldier[0].layer = gameObject.layer;
                    }

                    else if (j == 1)
                    {
                        soldier[1] = Instantiate(soldierprefab[1], center.transform.position, Quaternion.identity);

                        soldier[1].transform.parent = gameObject.transform;

                        soldier[1].layer = gameObject.layer;
                    }

                    else if (j == 2)
                    {
                        soldier[2] = Instantiate(soldierprefab[1], right.transform.position, Quaternion.identity);

                        soldier[2].transform.parent = gameObject.transform;

                        soldier[2].layer = gameObject.layer;
                    }
                }
                
            }
        }

        public void FillNotFullGroup(int count)
        {
            for (int j = 0; j < count; j++)
            {
                if (gameObject.CompareTag("Castle") || gameObject.CompareTag("Archer"))
                {
                    if (j == 0)
                    {
                        soldier[0] = Instantiate(soldierprefab[0], left.transform.position, Quaternion.identity);

                        soldier[0].transform.parent = gameObject.transform;

                        soldier[0].layer = gameObject.layer;
                    }

                    else if (j == 1)
                    {
                        soldier[1] = Instantiate(soldierprefab[0], center.transform.position, Quaternion.identity);

                        soldier[1].transform.parent = gameObject.transform;

                        soldier[1].layer = gameObject.layer;
                    }
                }

                else
                {
                    if (j == 0)
                    {
                        soldier[0] = Instantiate(soldierprefab[1], left.transform.position, Quaternion.identity);

                        soldier[0].transform.parent = gameObject.transform;

                        soldier[0].layer = gameObject.layer;
                    }

                    else if (j == 1)
                    {
                        soldier[1] = Instantiate(soldierprefab[1], center.transform.position, Quaternion.identity);

                        soldier[1].transform.parent = gameObject.transform;

                        soldier[1].layer = gameObject.layer;
                    }
                }
            }
        }

        public void SetCastles(GameObject start, GameObject end)
        {
            startCastles = start;

            endCastle = end;

            for (int i = 0; i < soldier.Length; i++)
            {
                if (soldier[i] != null)
                {
                    DestroySoldier destroySoldier = soldier[i].GetComponent<DestroySoldier>();

                    destroySoldier.SetCastles(startCastles, endCastle);
                }

                else
                {
                    continue;
                }
                
            }
            
        }

        internal void SetLayer(GameObject startCastles)
        {
            for (int i = 0; i < soldier.Length; i++)
            {
                if (soldier[i] != null)
                {
                    soldier[i].gameObject.layer = startCastles.gameObject.layer;
                }

                else { continue; }
                
            }
        }

        internal void SetFlags(bool flag)
        {
            isFootman = flag;
        }
    }
}
