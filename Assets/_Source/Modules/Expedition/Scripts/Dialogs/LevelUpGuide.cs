using Lean.Localization;
using PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class LevelUpGuide : MonoBehaviour
    {
        internal int state = 0;

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
        private GameManager gameManager;

        [SerializeField]
        private LevelController levelController;

        [SerializeField]
        private Text text;

        private bool tipFlag = false;

        private bool tipFlag1 = false;

        private bool levelFlag = false;

        private Animator animator;

        private int queue = 0;

        [SerializeField]
        private GameObject _animator;

        [SerializeField]
        private ConquestLevelsCollection levelsCollection;

        private void Start()
        {
            if (!levelsCollection.CollectionValue.First(n => n.Level == 2).Finished)
            {
                StartCoroutine(SetActiveGuide(1));
                animator = hand.GetComponent<Animator>();
            }
            

        }

        internal void DisableObject()
        {
            foreach (var castle in gameManager.castle)
            {
                castle.SetActive(true);
            }

            guide.SetActive(false);
        }

        private void Update()
        {

            if (gameManager.castle[3].layer == LayerMask.NameToLayer("Player") && tipFlag1 == false && state ==2)
            {
                StartCoroutine(SetActiveGuide(1));
                tipFlag1 = true;
            }

            else if (levelController.castleLevel[1] == 2 && levelFlag == false && state == 1)
            {
                StartCoroutine(SetActiveGuide(3));
                levelFlag = true;
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
            switch (state)
            {
                case 0:
                    guideBg.SetActive(true);

                    this._animator.SetActive(true);

                    Animator _animator = this._animator.GetComponent<Animator>();

                    arrow.SetActive(false);

                    arrow1.SetActive(false);

                    _animator.Play("touch");

                    hand.SetActive(false);

                    UpdateText(0);
                    break;
                case 1:
                    this._animator.SetActive(false);
                    guideBg.SetActive(true);
                    arrow.SetActive(true);
                    arrow1.SetActive(true);
                    hand.SetActive(true);
                    InvokeRepeating("RepeatAnim", 0, 1.6f);
                    UpdateText(1);
                    break;
                case 2:
                    guideBg.SetActive(true);
                    arrow.SetActive(false);
                    arrow1.SetActive(false);
                    hand.SetActive(false);
                    UpdateText(2);
                    break;
                
            }
        }

        internal IEnumerator SetActiveGuide(int sec)
        {
            yield return new WaitForSeconds(sec);

            guide.SetActive(true);
        }

        private void SetActivity()
        {
            guide.SetActive(true);
        }

        private void UpdateText(int index)
        {
            string localizedText = LeanLocalization.GetTranslationText(phrases[index].name);

            text.text = string.Format(localizedText);
        }
        void RepeatAnim()
        {

            if (queue % 2 == 0)
            {
                animator.SetInteger("State", 0);

                animator.Play("State");
            }

            else
            {
                animator.SetInteger("State", 1);

                animator.Play("State");
            }
            
            queue++;
            
        }
    }
}
