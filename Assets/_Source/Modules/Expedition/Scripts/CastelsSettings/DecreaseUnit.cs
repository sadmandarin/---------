using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DecreaseUnit : MonoBehaviour
{
    private Animator animator;

    //[SerializeField]
    //private Text text;

    void OnEnable()
    {
        animator = GetComponent<Animator>();

        // Подписываемся на событие окончания анимации
        animator.enabled = false;

        // При включении объекта воспроизводим анимацию
        //if (gameObject.activeSelf == true)
        //{
        //    animator.enabled = true;

        //    animator.Play("New State");
        //}

        StartCoroutine(PlayAnimationRepeatedly());
    }

    IEnumerator PlayAnimationRepeatedly()
    {
        while (true)
        {
            // Запускаем анимацию
            animator.enabled=true;

            // Ждем 1 секунду
            yield return new WaitForSeconds(0.5f);

            gameObject.SetActive(false);

            // Останавливаем анимацию (если она зациклена, это может быть необходимо)
            animator.enabled = false;
        }
    }

    //public void SetDecrease(int text)
    //{
    //    this.text.text = $"-{text}";
    //}
}
