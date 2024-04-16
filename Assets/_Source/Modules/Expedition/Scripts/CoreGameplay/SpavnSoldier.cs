using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

namespace Expedition
{
    public class SpavnSoldier : MonoBehaviour
    {
        [SerializeField]
        private GameObject soldierPrefab;

        private float _spawnRate = 0.35f;

        public IEnumerator SpawnSoldier(Vector3 position, float count, GameObject startCastles, GameObject endCastle, 
                                        float delay, GameObject[] castlesToGoTrough)
        {
            for (int i = 0; i < (int)(count/3); i++)
            {
                if (startCastles.tag == "Castle" || startCastles.tag == "Archer")
                {
                    GameObject soldier = Instantiate(soldierPrefab, position, Quaternion.identity);

                    soldier.tag = startCastles.tag;

                    SpawnGroup spawnGroup = soldier.GetComponent<SpawnGroup>();

                    spawnGroup.SetFlags(true);

                    spawnGroup.FillGroup();

                    SoldierMove(position, soldier, startCastles, endCastle, delay, castlesToGoTrough);
                }

                else if (startCastles.tag == "Barracks")
                {
                    GameObject soldier = Instantiate(soldierPrefab, position, Quaternion.identity);

                    soldier.tag = startCastles.tag;

                    SpawnGroup spawnGroup = soldier.GetComponent<SpawnGroup>();

                    spawnGroup.SetFlags(true);

                    spawnGroup.FillGroup();
                    SoldierMove(position, soldier, startCastles, endCastle, delay, castlesToGoTrough);
                }
                
                yield return new WaitForSeconds(_spawnRate);

            }
            if (count % 3 != 0)
            {
                if (startCastles.tag == "Castle" || startCastles.tag == "Archer")
                {
                    GameObject soldier = Instantiate(soldierPrefab, position, Quaternion.identity);

                    soldier.tag = startCastles.tag;

                    SpawnGroup spawnGroup = soldier.GetComponent<SpawnGroup>();

                    spawnGroup.SetFlags(true);

                    spawnGroup.FillNotFullGroup((int)(count % 3));

                    SoldierMove(position, soldier, startCastles, endCastle, delay, castlesToGoTrough);
                }

                else if (startCastles.tag == "Barracks")
                {
                    GameObject soldier = Instantiate(soldierPrefab, position, Quaternion.identity);

                    soldier.tag = startCastles.tag;

                    SpawnGroup spawnGroup = soldier.GetComponent<SpawnGroup>();

                    spawnGroup.SetFlags(true);

                    spawnGroup.FillNotFullGroup((int)(count % 3));
                    SoldierMove(position, soldier, startCastles, endCastle, delay, castlesToGoTrough);
                }
            }
        }

        void SoldierMove(Vector3 position, GameObject soldier, GameObject startCastles, GameObject endCastle, float delay, GameObject[] castlesToGoTrough)
        {
            SpawnGroup spawnGroup = soldier.GetComponent<SpawnGroup>();
            spawnGroup.SetCastles(startCastles, endCastle);
            spawnGroup.SetLayer(startCastles);
            spawnGroup.SetFlags(false);

            soldier.transform.position = position;

            Sequence soldierMovingSequence = DOTween.Sequence();

            GameObject currentCastle = startCastles;

            foreach (var nextCastle in castlesToGoTrough)
            {
                if (currentCastle != nextCastle)
                {
                    if (startCastles.tag == "Castle" || startCastles.tag == "Archer")
                    {
                        float dist = Vector3.Distance(currentCastle.transform.position, nextCastle.transform.position);
                        float speed = 2;

                        delay = dist / speed;
                    }

                    else
                    {
                        float dist = Vector3.Distance(currentCastle.transform.position, nextCastle.transform.position);
                        float speed = 1.4f;

                        delay = dist / speed;
                    }
                    

                    soldierMovingSequence.AppendCallback(() => soldier.transform.LookAt(nextCastle.transform))
                                        .Append(soldier.transform.DOMove(nextCastle.transform.position, delay).SetEase(Ease.Linear));

                    currentCastle = nextCastle;
                }
            }

            soldierMovingSequence.Play();


        }

        
    }
}
