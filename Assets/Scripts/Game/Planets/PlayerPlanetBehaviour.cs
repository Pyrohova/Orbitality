using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetBehaviour : MonoBehaviour, IPlanetBehaviour
{
    private float cooldown = 0;
    private float currentcooldown;
    private bool isActive = false;
    private RocketType rocketType;

    private RocketPool rocketPool;

    public void Initialize(float cooldown, RocketType rocketType)
    {
        this.cooldown = cooldown;
        this.rocketType = rocketType;
        isActive = true;
    }

    public void Shoot()
    {
        Vector2 shotDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;

        //Shoot Requested
        if (Input.GetMouseButtonDown(0))
        {
            rocketPool.AcquireRocket(rocketType, transform.position, shotDirection);
        }
    }

    public void UpdateCooldown()
    {
        if (isActive)
        {
            currentcooldown -= Time.deltaTime;
            if (currentcooldown <= 0)
            {
                Shoot();
                currentcooldown = cooldown;
            }
        }
    }
}
