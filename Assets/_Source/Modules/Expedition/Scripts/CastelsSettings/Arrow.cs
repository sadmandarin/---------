using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Expedition
{
    public class Arrow : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Footman" || other.gameObject.tag == "Hoplites")
            {
                Destroy(other.gameObject);

                Destroy(gameObject);
            }
        }
    }
}
