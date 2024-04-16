using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class ButtonLevelUp : MonoBehaviour
    {
        private float lastClickTime = 0f;

        private float doubleClick = 0.35f;
        [SerializeField]
        internal int _castleIndex;

        public LevelController levelController;

        [SerializeField]
        private LayerMask layerMask;

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            for (int i = 0; i < gameManager.castle.Length; i++)
            {
                if (gameObject == gameManager.castle[i].gameObject)
                {
                    _castleIndex = i;
                }
            }
        }

        private void Start()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                float currentTime = Time.time;

                if (currentTime - lastClickTime < doubleClick)
                {
                    // Это двойной клик
                    GetCastle();

                    lastClickTime = 0f;
                }
                else
                {
                    // Это обычный клик
                    lastClickTime = currentTime;
                }
            }
        }

        private void GetCastle()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
            {
                var index = hit.collider.gameObject.GetComponent<Attacks>();
                    
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") && !(hit.collider.gameObject.tag == "Footman" || hit.collider.gameObject.tag == "Hoplites") && hit.collider.gameObject.tag != "TowerRange")
                    {
                        if (levelController.blueAnim[index.castleIndex].activeSelf != true)
                        {
                            levelController.IncreaseLevel(index.castleIndex);
                            
                        }
                    }
                    
                    
                
            }
        }

        private bool IsClickInsideBoxCollider(Collider collider)
        {
            // Проверяем, является ли переданный коллайдер BoxCollider
            BoxCollider boxCollider = collider as BoxCollider;
            return boxCollider != null;
        }
    }
}
