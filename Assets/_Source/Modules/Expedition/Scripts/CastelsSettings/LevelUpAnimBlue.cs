using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class LevelUpAnimBlue : MonoBehaviour
    {
        // Start is called before the first frame update
        private void OnEnable()
        {
            StartCoroutine(TimerAnimBlue(2f));
        }

        // Update is called once per frame
        IEnumerator TimerAnimBlue(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            gameObject.SetActive(false);
        }
    }
}
