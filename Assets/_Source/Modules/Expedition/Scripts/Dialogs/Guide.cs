using Lean.Localization;
using PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class Guide : MonoBehaviour
    {
        internal int _state = 0;

        [SerializeField]
        private LeanPhrase[] phrases;

        [SerializeField]
        internal GameObject guide;

        [SerializeField]
        private GameObject arrow;

        [SerializeField]
        private GameObject arrow1;

        [SerializeField]
        private GameObject hand;

        [SerializeField]
        private GameObject guideBg;

        [SerializeField]
        private GameObject neutralityCamp;

        [SerializeField]
        private GameObject playerCamp;

        [SerializeField]
        private GameObject enemyCamp;

        [SerializeField]
        private GameManager gameManager;

        [SerializeField] 
        private Text text;

        private bool tipFlag = false;

        [SerializeField]
        private GameObject startAnimation;

        [SerializeField]
        private ConquestLevelsCollection levelsCollection;

        private void Start()
        {
            if (!levelsCollection.CollectionValue.First(n => n.Level == 1).Finished) 
            {
                gameObject.SetActive(true);

                SetObject();

                Time.timeScale = 0f;
            }

            else
            {
                gameObject.SetActive(false);

                startAnimation.SetActive(true);
            }
        }

        internal void DisableObject()
        {
            guide.SetActive(false);
            gameManager.SetIsGameOnPause(false);
            foreach (var castle in gameManager.castle)
            {
                castle.SetActive(true);
            }

            if (_state == 1 || _state == 3) 
            {
                StartCoroutine(SetActiveGuide());
            }
            
        }

        private void Update()
        {
            if (gameManager.castle[1].layer == LayerMask.NameToLayer("Player") && tipFlag == false)
            {
                SetActive();
                tipFlag = true;
            }

            if (guide.activeSelf == true)
            {
                foreach (var castle in gameManager.castle)
                {
                    castle.SetActive(false);
                }
            }
        }

        internal void SetObject()
        {
            Animator animator = hand.GetComponent<Animator>();
            gameManager.SetIsGameOnPause(true);
            switch (_state)
            { 
                case 0:
                    guide.SetActive(true);
                    guideBg.SetActive(true);
                    neutralityCamp.SetActive(true);
                    arrow.SetActive(false);
                    arrow1.SetActive(false);
                    playerCamp.SetActive(true);
                    enemyCamp.SetActive(true);
                    UpdateText(0);
                    break;

                case 1:
                    guideBg.SetActive(true);
                    arrow.SetActive(true);
                    hand.SetActive(true);
                    arrow1.SetActive(false);
                    animator.SetInteger("State", 1);
                    animator.Play("State");
                    neutralityCamp.SetActive(false);
                    playerCamp.SetActive(false);
                    enemyCamp.SetActive(false);
                    UpdateText(1);
                    break;
                case 2:
                    guideBg.SetActive(true);
                    arrow.SetActive(false);
                    arrow1.SetActive(false);
                    hand.SetActive(false);
                    UpdateText(2);
                    break;
                case 3:
                    arrow.SetActive(true);
                    arrow1.SetActive(true);
                    
                    hand.SetActive(true);
                    animator.SetInteger("State", 3);
                    animator.Play("State");
                    guideBg.SetActive(true);
                    UpdateText(3);
                    break;
            }
        }

        internal IEnumerator SetActiveGuide()
        {
            yield return new WaitForSeconds(1);

            guide.SetActive(true);
        }

        void SetActive()
        {
            guide.SetActive(true);
        }

        private void UpdateText(int index)
        {
            string localizedText = LeanLocalization.GetTranslationText(phrases[index].name);

            text.text = string.Format(localizedText);
        }
    }
}
