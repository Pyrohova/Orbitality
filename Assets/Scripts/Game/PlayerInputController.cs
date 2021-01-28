using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 shotDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;

        //Shoot Requested
        if (Input.GetMouseButtonDown(0))
        {
            //shoot;
        }
    }
}
