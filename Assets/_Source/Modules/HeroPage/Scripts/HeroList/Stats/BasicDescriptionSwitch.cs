using UnityEngine;

namespace HeroPage
{
    internal class BasicDescriptionSwitch : MonoBehaviour
    {
        [SerializeField] private BasicDescriptionBubble _bubble;

        private void Update()
        {
            if(_bubble.IsActive && Input.GetMouseButtonDown(0))
            {
                _bubble.Hide();
            }

        }
    }
}
