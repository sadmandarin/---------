using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class ChangeLevelAnim : MonoBehaviour
    {
        
        void OnEnable()
        { 

            StartCoroutine(StartTimer(8f));

            
        }

        IEnumerator StartTimer(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            gameObject.SetActive(false);
        }
    }
}
