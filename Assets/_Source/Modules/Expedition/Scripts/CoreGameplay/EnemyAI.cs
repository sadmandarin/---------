using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Expedition
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private float _attackProbability = 0.3f;

        [SerializeField]
        private float _levelUpProbability = 0.7f;

        private float waitForFill = 0.7f;

        private LevelController levelController;

        private AddUnitToCastle addUnit;

        private GameManager gameManager;

        CastleNode castleNode;

        private void Start()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

            addUnit = GameObject.Find("LevelController").GetComponent<AddUnitToCastle>();

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            castleNode = GetComponent<CastleNode>();

            StartCoroutine(PerformActions());

            
        }

        void ChooseCastle(float randomValue, float doubleAttack, CastleNode node)
        {
            List<CastleNode> connectedCastles = new List<CastleNode>(node.connectedCastles);

            // ”бираем текущий замок из списка, чтобы избежать повторного выбора
            connectedCastles.Remove(node);

            List<CastleNode> filteredCastle = connectedCastles.Where(item => item.gameObject.layer != LayerMask.NameToLayer("Enemy")).ToList();

            var randomBetween = Random.Range(0, filteredCastle.Count);

            if (filteredCastle.Count > 0)
            {
                if (filteredCastle[randomBetween].gameObject.layer != LayerMask.NameToLayer("Between") && filteredCastle[randomBetween].gameObject.layer != LayerMask.NameToLayer("Enemy"))
                {
                    CastleNode targetCastleNode = filteredCastle[randomBetween];

                    if (addUnit.currentSoldiers[targetCastleNode.gameObject.GetComponent<Attacks>().castleIndex] <= addUnit.currentSoldiers[gameObject.GetComponent<Attacks>().castleIndex] / 1.6f || addUnit.currentSoldiers[targetCastleNode.gameObject.GetComponent<Attacks>().castleIndex] >= 40)
                    {
                        GameObject targetCastle = targetCastleNode.gameObject;

                        if (randomValue < _attackProbability)
                        {
                            //if (doubleAttack > 0.5f)
                            //{
                            //    gameManager.MoveSoldier(gameObject, targetCastle);
                            //}
                            gameManager.MoveSoldier(gameObject, targetCastle);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    // ѕродолжаем поиск, если атака не выполнена
                    randomValue = Random.value;
                    ChooseCastle(randomValue, doubleAttack, filteredCastle[randomBetween]);
                }

            }
            else
            {
                TransferToNonEnemyCastle(node);
                return;
            }
        }

        void TransferToNonEnemyCastle(CastleNode node)
        {
            List<CastleNode> connectedNonEnemyCastles = new List<CastleNode>();

            foreach (var connectedCastle in node.connectedCastles)
            {
                // ѕровер€ем, что у connectedCastle хот€ бы один замок с layer не Enemy
                if (connectedCastle.connectedCastles.Any(c => c.gameObject.layer != LayerMask.NameToLayer("Enemy")))
                {
                    connectedNonEnemyCastles.Add(connectedCastle);

                }
            }

            if (connectedNonEnemyCastles.Count > 0)
            {
                var randomTargetCastle = connectedNonEnemyCastles[Random.Range(0, connectedNonEnemyCastles.Count)];

                gameManager.MoveSoldier(gameObject, randomTargetCastle.gameObject);
            }
            else
            {
                if (node.gameObject.layer != LayerMask.NameToLayer("Between"))
                {
                    var randomTargetCastle = node.connectedCastles[Random.Range(0, node.connectedCastles.Count)];

                    gameManager.MoveSoldier(gameObject, randomTargetCastle.gameObject);
                }

                else
                {
                    return;

                }
                
            }
        }

        IEnumerator PerformActions()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(3f, 6f));

                int index = GetComponent<Attacks>().castleIndex;

                Attacks attacks = GetComponent<Attacks>();

                float randomValue = Random.value;

                float doubleAttack = Random.value;

                int currLvl = levelController.GetCurrentLevel(index);

                if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    var random = Random.Range(0, castleNode.connectedCastles.Count);

                    GameObject targetCastle = castleNode.connectedCastles[random].gameObject;


                    if (addUnit.currentSoldiers[index] >= levelController.maxSoldierForLevel[levelController.castleLevel[index] - 1] && randomValue < _attackProbability)
                    {
                        ChooseCastle(randomValue, doubleAttack, castleNode);
                    }


                    else if (targetCastle != null && randomValue < _attackProbability)
                    {
                        ChooseCastle(randomValue, doubleAttack, castleNode);
                    }

                    

                    if (randomValue > _levelUpProbability && levelController.anim[index].activeSelf == false && addUnit.currentSoldiers[index] >= levelController.soldierNextLvl[currLvl - 1])
                    {
                        levelController.IncreaseLevel(index);
                    }

                    else if (randomValue >= waitForFill)
                    {
                        while (addUnit.currentSoldiers[gameObject.GetComponent<Attacks>().castleIndex] < levelController.maxSoldierForLevel[levelController.castleLevel[gameObject.GetComponent<Attacks>().castleIndex] - 1] / 2)
                        {
                            yield return null; // ѕодождать следующего кадра
                        }
                    }
                    

                    if (levelController.GetCurrentLevel(index) == 5)
                    {
                        _attackProbability = 0.5f;

                        _levelUpProbability = 0f;
                    }
                }

                yield return null;
            }

        }
        }
    }


