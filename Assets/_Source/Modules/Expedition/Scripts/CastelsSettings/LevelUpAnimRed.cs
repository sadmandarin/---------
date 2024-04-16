using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class LevelUpAnimRed : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(TimerAnimRed(2f));
        }

        // Update is called once per frame
        IEnumerator TimerAnimRed(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            gameObject.SetActive(false);
        }
    }
}
