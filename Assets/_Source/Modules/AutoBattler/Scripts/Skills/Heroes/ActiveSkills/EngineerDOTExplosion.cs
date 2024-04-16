using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    internal class EngineerDOTExplosion : MonoBehaviour
    {
        [SerializeField] private float _time;
        private void Awake()
        {
            StartCoroutine(DestroyAfterTime());
        }

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(_time);
            Destroy(gameObject);
        }
    }
}
