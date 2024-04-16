using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObject : MonoBehaviour
{
    internal Action<Transform> OnClicked;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Button _button;

    private bool _isClicked;

    // Если игру на паузу ставить будешь - поменяй на update, да будет от фпс зависить, но fixed вообще работать не будет
    private void FixedUpdate()
    {
        if (_isClicked)
        {
            return;
        }
        transform.position += Vector3.up * (moveSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(PressItem);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PressItem);
    }

    private void PressItem()
    {
        _isClicked = true;
        _button.enabled = false;
        OnClicked?.Invoke(transform);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
