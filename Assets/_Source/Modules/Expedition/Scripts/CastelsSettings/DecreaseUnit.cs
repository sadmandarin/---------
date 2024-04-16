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

        // ������������� �� ������� ��������� ��������
        animator.enabled = false;

        // ��� ��������� ������� ������������� ��������
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
            // ��������� ��������
            animator.enabled=true;

            // ���� 1 �������
            yield return new WaitForSeconds(0.5f);

            gameObject.SetActive(false);

            // ������������� �������� (���� ��� ���������, ��� ����� ���� ����������)
            animator.enabled = false;
        }
    }

    //public void SetDecrease(int text)
    //{
    //    this.text.text = $"-{text}";
    //}
}
