using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Expedition
{
    public class ChangeProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Sprite playerProgressBarSprite;

        [SerializeField]
        private Sprite enemyProgressBarSprite;

        [SerializeField]
        private GameObject building;

        private Image imageRenderer;

        private void Start()
        {
            imageRenderer = GetComponent<Image>();
        }

        void Update()
        {
            if (building.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                imageRenderer.sprite = playerProgressBarSprite;
            }

            else if (building.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                imageRenderer.sprite = enemyProgressBarSprite;
            }
        }
    }
}
