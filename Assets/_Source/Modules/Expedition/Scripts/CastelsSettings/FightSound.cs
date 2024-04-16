using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Expedition
{
    public class FightSound : MonoBehaviour
    {
        [SerializeField]
        public AudioClip soundClip;

        AudioSource source;

        Coroutine coroutine;

        private List<GameObject> collidingObjects = new List<GameObject>();

        private HashSet<GameObject> soldiersInZone = new HashSet<GameObject>();

        private void OnEnable()
        {
            source = GameObject.Find("FightSound").GetComponent<AudioSource>();
        }
       
    }
}
