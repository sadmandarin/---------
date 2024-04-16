using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class AlchemistPoisonBomb : MonoBehaviour
    {
        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}
