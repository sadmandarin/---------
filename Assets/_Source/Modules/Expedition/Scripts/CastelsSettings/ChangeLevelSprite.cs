using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class ChangeLevelSprite : MonoBehaviour
    {
        [SerializeField]
        private AudioClip buildClip;

        [SerializeField]
        private AudioClip levelUpClip;

        [SerializeField]
        private AudioClip occupyClip;

        private List<AudioSource> audioSources = new List<AudioSource>();

        public SpriteRenderer[] levelSprite;

        public GameObject[] gameObjects;

        public Sprite[] enemyArcherTower;

        public Sprite changeCastle;

        public Sprite changeArcher;

        public Sprite[] playerArcherTower;

        public Sprite[] enemyBarracksTower;

        public Sprite[] playerBarracksTower;

        public Sprite[] enemyCastleTower;

        public Sprite[] playerCastleTower;

        public Sprite[] neutralBarracksTower;

        public Sprite[] neutralCastleTower;

        public Sprite[] neutralArcherTower;

        private LevelController levelController;

        private float[] animTime = { 0, 3, 4, 6, 7 };

        private GameManager gameManager;

        Coroutine[] startSpriteTimer;

        Coroutine blue;

        Coroutine red;

        // Start is called before the first frame update

        private void Start()
        {
            levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            startSpriteTimer = new Coroutine[gameObjects.Length];

            for (int i = 0; i < gameManager.castle.Length; i++)
            {
                var level = levelController.GetCurrentLevel(i) - 1;

                ChangeSpriteFirstTime(i, level);
            }

            foreach (var source in gameObjects)
            {
                AudioSource sourceAudio = source.GetComponent<AudioSource>();

                audioSources.Add(sourceAudio);
            }
        }

        public void ChangeSprites(int index, int currentLevel)
        {
            if (gameObjects[index].tag == "Castle" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                levelSprite[index].sprite = changeCastle;

                
                StartCoroutine(PlayBuildAnim(index));

                StartCoroutine(PlaySoundRepeatedly(index, buildClip));

                if (startSpriteTimer[index] == null)
                {
                    startSpriteTimer[index] = StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
                else
                {
                    StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
            }

            else if (gameObjects[index].tag == "Barracks" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                levelSprite[index].sprite = changeCastle;

                StartCoroutine(PlayBuildAnim(index));

                StartCoroutine(PlaySoundRepeatedly(index, buildClip));

                if (startSpriteTimer[index] == null)
                {
                    startSpriteTimer[index]  = StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
                else
                {
                    StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
            }

            else if (gameObjects[index].tag == "Archer" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                levelSprite[index].sprite = changeArcher;

                StartCoroutine(PlayBuildAnim(index));

                StartCoroutine(PlaySoundRepeatedly(index, buildClip));

                if (startSpriteTimer[index] == null)
                {
                    startSpriteTimer[index] = StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
                else
                {
                    StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
            }

            else if (gameObjects[index].tag == "Castle" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                levelSprite[index].sprite = changeCastle;

                StartCoroutine(PlayBuildAnim(index));

                StartCoroutine(PlaySoundRepeatedly(index, buildClip));

                if (startSpriteTimer[index] == null)
                {
                    startSpriteTimer[index] = StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
                else
                {
                    StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
            }

            else if (gameObjects[index].tag == "Barracks" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                levelSprite[index].sprite = changeCastle;

                StartCoroutine(PlayBuildAnim(index));

                StartCoroutine(PlaySoundRepeatedly(index, buildClip));

                if (startSpriteTimer[index] == null)
                {
                    startSpriteTimer[index] = StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
                else
                {
                    StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
            }

            else if (gameObjects[index].tag == "Archer" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                levelSprite[index].sprite = changeArcher;

                StartCoroutine(PlayBuildAnim(index));

                StartCoroutine(PlaySoundRepeatedly(index, buildClip));

                if (startSpriteTimer[index] == null)
                {
                    startSpriteTimer[index] = StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
                else
                {
                    StartCoroutine(StartSpriteTimer(animTime[currentLevel - 1], index, currentLevel - 1));
                }
            }
        }

        void ChangeSpriteFirstTime(int index, int currentLevel)
        {
            if (gameObjects[index].tag == "Castle" && (gameObjects[index].layer == LayerMask.NameToLayer("Player")))
            {
                levelSprite[index].sprite = playerCastleTower[currentLevel];
            }

            else if (gameObjects[index].tag == "Barracks" && (gameObjects[index].layer == LayerMask.NameToLayer("Player")))
            {
                levelSprite[index].sprite = playerBarracksTower[currentLevel];
            }

            else if (gameObjects[index].tag == "Archer" && (gameObjects[index].layer == LayerMask.NameToLayer("Player")))
            {
                levelSprite[index].sprite = playerArcherTower[currentLevel];
            }

            else if (gameObjects[index].tag == "Castle" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy")))
            {
                levelSprite[index].sprite = enemyCastleTower[currentLevel];
            }

            else if (gameObjects[index].tag == "Barracks" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy")))
            {
                levelSprite[index].sprite = enemyBarracksTower[currentLevel];
            }

            else if (gameObjects[index].tag == "Archer" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy")))
            {
                levelSprite[index].sprite = enemyArcherTower[currentLevel];
            }

            if (gameObjects[index].tag == "Castle" && gameObjects[index].layer == LayerMask.NameToLayer("Neutral"))
            {
                levelSprite[index].sprite = neutralCastleTower[currentLevel];
            }

            else if (gameObjects[index].tag == "Barracks" && gameObjects[index].layer == LayerMask.NameToLayer("Neutral"))
            {
                levelSprite[index].sprite = neutralBarracksTower[currentLevel];
            }

            else if (gameObjects[index].tag == "Archer" && gameObjects[index].layer == LayerMask.NameToLayer("Neutral"))
            {
                levelSprite[index].sprite = neutralArcherTower[currentLevel];
            }
        }

        public void SpriteDelay(int index, int currentLevel)
        {
            if (gameObjects[index].tag == "Castle" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = playerCastleTower[currentLevel];

                levelController.blueAnim[index].SetActive(true);

                audioSources[index].PlayOneShot(levelUpClip);

                levelController.UpdateLevelText(index, currentLevel);

                blue = StartCoroutine(BlueLevelUpAnim(index));

                startSpriteTimer[index] = null;

            }

            else if (gameObjects[index].tag == "Barracks" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = playerBarracksTower[currentLevel];

                levelController.blueAnim[index].SetActive(true);

                audioSources[index].PlayOneShot(levelUpClip);

                levelController.UpdateLevelText(index, currentLevel);

                blue = StartCoroutine(BlueLevelUpAnim(index));

                startSpriteTimer[index] = null;

            }

            else if (gameObjects[index].tag == "Archer" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = playerArcherTower[currentLevel ];

                levelController.blueAnim[index].SetActive(true);

                audioSources[index].PlayOneShot(levelUpClip);

                levelController.UpdateLevelText(index, currentLevel);

                blue = StartCoroutine(BlueLevelUpAnim(index));

                startSpriteTimer[index] = null;

            }

            else if (gameObjects[index].tag == "Castle" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = enemyCastleTower[currentLevel];

                levelController.redAnim[index].SetActive(true);

                audioSources[index].PlayOneShot(levelUpClip);

                levelController.UpdateLevelText(index, currentLevel);

                red = StartCoroutine(RedLevelUpAnim(index));

                startSpriteTimer[index] = null;

            }

            else if (gameObjects[index].tag == "Barracks" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = enemyBarracksTower[currentLevel];

                levelController.redAnim[index].SetActive(true);

                audioSources[index].PlayOneShot(levelUpClip);
                
                levelController.UpdateLevelText(index, currentLevel);

                red = StartCoroutine(RedLevelUpAnim(index));

                startSpriteTimer[index] = null;


            }

            else if (gameObjects[index].tag == "Archer" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = enemyArcherTower[currentLevel];

                levelController.redAnim[index].SetActive(true);

                audioSources[index].PlayOneShot(levelUpClip);

                levelController.UpdateLevelText(index, currentLevel);

                red = StartCoroutine(RedLevelUpAnim(index));

                startSpriteTimer[index] = null;
            }
        }

        public void ChandeLayer(int index, int currentLevel)
        {
            if (gameObjects[index].tag == "Castle" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = playerCastleTower[currentLevel];
                audioSources[index].PlayOneShot(occupyClip);

                levelController.UpdateLevelText(index, currentLevel);
                startSpriteTimer[index] = null;

            }

            else if (gameObjects[index].tag == "Barracks" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = playerBarracksTower[currentLevel];
                audioSources[index].PlayOneShot(occupyClip);

                levelController.UpdateLevelText(index, currentLevel);
                startSpriteTimer[index] = null;

            }

            else if (gameObjects[index].tag == "Archer" && (gameObjects[index].layer == LayerMask.NameToLayer("Player") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = playerArcherTower[currentLevel];

                audioSources[index].PlayOneShot(occupyClip);

                levelController.UpdateLevelText(index, currentLevel);
                startSpriteTimer[index] = null;

            }

            else if (gameObjects[index].tag == "Castle" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = enemyCastleTower[currentLevel];

                audioSources[index].PlayOneShot(occupyClip);

                levelController.UpdateLevelText(index, currentLevel);

                startSpriteTimer[index] = null;

            }

            else if (gameObjects[index].tag == "Barracks" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = enemyBarracksTower[currentLevel];

                audioSources[index].PlayOneShot(occupyClip);

                levelController.UpdateLevelText(index, currentLevel);

                startSpriteTimer[index] = null;


            }

            else if (gameObjects[index].tag == "Archer" && (gameObjects[index].layer == LayerMask.NameToLayer("Enemy") || gameObjects[index].layer == LayerMask.NameToLayer("Neutral")))
            {
                audioSources[index].Stop();

                levelController.anim[index].SetActive(false);

                levelSprite[index].sprite = enemyArcherTower[currentLevel];

                audioSources[index].PlayOneShot(occupyClip);

                levelController.UpdateLevelText(index, currentLevel);

                startSpriteTimer[index] = null;
            }
        }

        public void CampChange(int index, int currentLevel)
        {
            if (startSpriteTimer[index] != null)
            {
                StopCoroutine(startSpriteTimer[index]);

                startSpriteTimer[index] = null;
            }

            levelController.campChange[index].SetActive(true);

            StartCoroutine(CampChange(index));

            ChandeLayer(index, currentLevel);

        }

        public IEnumerator StartSpriteTimer(float seconds, int index, int currentLevel)
        {
            yield return new WaitForSeconds(seconds);

            SpriteDelay(index, currentLevel);

            //levelController.UpdateLevelText(index, currentLevel);
        }

        IEnumerator PlaySoundRepeatedly(int index, AudioClip soundClip)
        {
            while (levelController.anim[index].activeSelf == true)
            {
                audioSources[index].PlayOneShot(soundClip);

                yield return new WaitForSeconds(0.45f);
            }
        }

        IEnumerator PlayBuildAnim(int index)
        {
            while (true)
            {
                levelController.anim[index].SetActive(true);

                yield return new WaitForSeconds(0.45f);

                levelController.anim[index].SetActive(false);

                if (levelController.campChange[index].activeSelf == true || levelController.redAnim[index].activeSelf == true || levelController.blueAnim[index].activeSelf == true)
                {
                    break;
                }
            }
        }

        IEnumerator BlueLevelUpAnim(int index)
        {
            yield return new WaitForSeconds(1f);

            levelController.blueAnim[index].SetActive(false);
        }

        IEnumerator RedLevelUpAnim(int index)
        {
            yield return new WaitForSeconds(1f);

            levelController.redAnim[index].SetActive(false);
        }
        
        IEnumerator CampChange(int index)
        {
            yield return new WaitForSeconds(1f);

            levelController.campChange[index].SetActive(false);
        }
    }
}
