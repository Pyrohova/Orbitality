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

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(GetMousePoint());
            OnPlayerClick?.Invoke(GetMousePoint());
        }
    }
}
