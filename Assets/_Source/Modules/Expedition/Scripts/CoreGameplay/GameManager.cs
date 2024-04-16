using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class GameManager : MonoBehaviour
    {
        public int level;

        private SpriteRenderer spriteRenderer;

        public GameObject arrowPrefab;

        public List<SpriteRenderer> arrows;

        public GameObject[] castle;

        public List<GameObject> castles;

        private GameObject startCastle;

        private GameObject endCastle;

        public Graph graph;

        private AddUnitToCastle addUnitToCastle;

        private SpavnSoldier spavnSoldier;

        public AudioClip audioClip;

        private AudioSource audioSource;

        private bool isGamePaused;

        [SerializeField]
        private LayerMask layerMask;

        private void OnEnable()
        {
            
            Time.timeScale = 1f;
            
        }

        private void Awake()
        {
            
            graph = GameObject.Find("Graph").GetComponent<Graph>();

            spavnSoldier = GetComponent<SpavnSoldier>();

            audioSource = GetComponent<AudioSource>();

            addUnitToCastle = GameObject.Find("LevelController").GetComponent<AddUnitToCastle>();

            for (int i = 0; i < castle.Length; i++)
            {
                graph.AddNode(castle[i]);
            }
        }

        private void Update()
        {
            if (isGamePaused)
                return;

            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                UpdateArrows();
            }

            if (Input.GetMouseButtonDown(1))
                Debug.Break();

            GetCastle();
        }

        void GetCastleUnderMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MouseAction"))
                {
                    var _castle = castle[hit.collider.gameObject.GetComponent<Attacks>().castleIndex];

                    if (_castle.layer == LayerMask.NameToLayer("Player") && !castles.Contains(_castle) && !(hit.collider.gameObject.tag == "Footman" || hit.collider.gameObject.tag == "Hoplites") && hit.collider.gameObject.tag != "TowerRange")
                    {
                        castles.Add(_castle.gameObject);

                        Debug.Log("_castle");

                        GameObject arrow = Instantiate(arrowPrefab);

                        arrows.Add(arrow.GetComponent<SpriteRenderer>());

                    }
                }
                
            }
        }

        GameObject GetEndCastle()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MouseAction") && !(hit.collider.gameObject.tag == "Footman" || hit.collider.gameObject.tag == "Hoplites"))
                {
                    var index = hit.collider.gameObject.GetComponent<Attacks>().castleIndex;
                    return castle[index];
                }
            }
            return null;
        }

        void GetCastle()
        {
            if (Input.GetMouseButton(0))
            {
                GetCastleUnderMouse();
            }

            if (Input.GetMouseButtonUp(0))
            {
                endCastle = GetEndCastle();

                if (castles != null && endCastle != null)
                {
                    foreach (var _castle in castles)
                    {
                        MoveSoldier(_castle, endCastle);
                    }
                    castles.Clear();

                    endCastle = null;
                }

                else if (endCastle == null)
                {
                    castles.Clear();
                }
                DeleteArrow();
            }
        }

        void DeleteArrow()
        {
            foreach(var arrow in arrows)
            {
                Destroy(arrow);
            }
            arrows.Clear(); 
        }

        public void MoveSoldier(GameObject start, GameObject end)
        {
            int attack = 0;

            var index1 = start.GetComponent<Attacks>();

            var _index = start.GetComponent<Attacks>().castleIndex;

            if (start.tag == addUnitToCastle.AddUnitToCastlePrefab[_index].tag && start != end)
            {
                var nodesToGoTrough = graph.FindAllNodesBetween(start.GetComponent<CastleNode>(), end.GetComponent<CastleNode>());
                var castlesToGoTrough = nodesToGoTrough.Select(n => n.gameObject).TakeLast(nodesToGoTrough.Count - 1).ToArray();

                if (castlesToGoTrough.Length != 0)
                {
                    Text harmText = addUnitToCastle.decreaseUnit[_index].GetComponent<Text>();

                    if (addUnitToCastle.currentSoldiers[_index] >= 2)
                    {
                        attack = (int)addUnitToCastle.currentSoldiers[_index] / 2;
                    }

                    else
                    {
                        attack = 0;
                    }
                    

                    addUnitToCastle.decreaseUnit[_index].SetActive(true);

                    harmText.text = string.Format("-" + attack);

                    

                    addUnitToCastle.currentSoldiers[_index] = addUnitToCastle.currentSoldiers[_index] - attack;

                    addUnitToCastle.UpdateUnitUI(_index, (int)(addUnitToCastle.currentSoldiers[_index]));

                    StartCoroutine(spavnSoldier.SpawnSoldier(start.transform.position, attack, start, end, 7f, castlesToGoTrough));

                    audioSource.clip = audioClip;

                    audioSource.Play();
                }
            }
        }

        void UpdateArrows()
        {
            if (arrows != null)
            {
                for (int i = 0; i < castles.Count; i++)
                {
                    Vector3 startPos = castles[i].transform.position;


                    Vector3 endPos = GetMousePosition();
                    
                    // Позиция стрелки равна середине между начальным и конечным замками
                    arrows[i].transform.position = Vector3.Lerp(startPos, endPos, 0.6f);

                    // Определяем направление от начального замка к конечному
                    Vector3 direction = (endPos - startPos).normalized;

                    // Вычисляем угол поворота для стрелки
                    float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90;

                    // Поворачиваем стрелку в нужное направление (горизонтально)
                    arrows[i].transform.rotation = Quaternion.Euler(90f, 0f, angle);

                    // Вычисляем длину стрелки
                    //float distance = Vector3.Distance(startPos, endPos) / 2;
                    float distance = Vector3.Distance(startPos, endPos);

                    // Изменяем размер спрайта стрелки
                    //arrows[i].transform.localScale = new Vector3(1f, distance, 1f);
                    arrows[i].GetComponent<SpriteRenderer>().size = new Vector2(1f, distance);

                    SpriteRenderer arrowSpriteRenderer = arrows[i].GetComponent<SpriteRenderer>();

                    arrowSpriteRenderer.sortingOrder = 0;

                    arrowSpriteRenderer.drawMode = SpriteDrawMode.Sliced;
                }
            
            }
        }

        public void SetIsGameOnPause(bool value)
        {
            isGamePaused = value;
            if (value == true)
                DeleteArrow();
        }

        private Vector3 GetMousePosition()
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MouseAction"))
                {
                    if (!hit.collider.CompareTag("Footman") && !hit.collider.CompareTag("Hoplites") && !hit.collider.CompareTag("TowerRange"))
                    {
                        return hit.collider.gameObject.transform.position;
                    }
                }

                return hit.point;
                
            }
            return Vector3.zero;
            
        }
    }

}

