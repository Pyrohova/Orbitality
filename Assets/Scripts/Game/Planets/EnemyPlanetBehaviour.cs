using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanetBehaviour : MonoBehaviour, IPlanetBehaviour
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
        rocketPool.AcquireRocket(rocketType, transform.position, transform.position.normalized);
    }

    public IEnumerator ShootAndReload()
    {
        yield return new WaitForSeconds(cooldown);
        Shoot();
    }

    public void UpdateCooldown()
    {
        if (isActive)
        {
            currentcooldown -= Time.deltaTime;
            if(currentcooldown <= 0)
            {
                Shoot();
                currentcooldown = cooldown;
            }
        }
    }

    private void Start()
    {
        rocketPool = ServiceLocator.GetInstance().GetRocketPool();
    }

    private void FixedUpdate()
    {
        UpdateCooldown();
    }
}
