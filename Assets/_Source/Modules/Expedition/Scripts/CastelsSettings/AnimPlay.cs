using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class AnimPlay : MonoBehaviour
    {
        [SerializeField]
        private GameObject anim;

        // Start is called before the first frame update
        private void OnEnable()
        {
            anim.SetActive(true);
        }
    }
}
