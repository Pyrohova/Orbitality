using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Action<Vector2> OnPlayerClick;

    public Vector2 GetDirection()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPlayerClick?.Invoke(GetDirection());
        }
    }
}
