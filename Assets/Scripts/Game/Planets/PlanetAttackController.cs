using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows current planet to attack other planets.
/// </summary>
public abstract class PlanetAttackController : MonoBehaviour
{
    protected RocketManager rocketManager;
    protected PlanetController planetController;

    protected RocketType rocketType;
    protected PlanetState planetState = PlanetState.ReadyToAttack;

    protected GameObject readyToAttackIcon;
    protected float cooldown;

    protected void Start()
    {
        rocketManager = ServiceLocator.GetInstance().GetRocketManager();
    }

    public void Initialize(RocketType rocketType, float cooldown, GameObject readyToAttackIcon)
    {
        this.rocketType = rocketType;
        this.cooldown = cooldown;
        this.readyToAttackIcon = readyToAttackIcon;
    }

    protected abstract IEnumerator ReloadShooting();

    public abstract void Shoot(Vector2 dir);
}
