using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Action<Vector2> OnPlayerClick;

    public Vector2 GetMousePoint()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector2 GetTouchPoint()
    {
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPlayerClick?.Invoke(GetMousePoint());
        }

        if (Input.touchCount > 0)
        {
            OnPlayerClick?.Invoke(GetTouchPoint());
        }
    }
}
