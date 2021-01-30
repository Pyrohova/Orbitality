using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInitializationValues
{
    public Sprite image;
    public float scale;

    public float speed;
    public Vector2 distanceToSun;
    public Vector3 sunPosition;

    public float maxHP;

    public RocketType rocketType;

    public float reloadingTime;
    public CooldownController cooldownController;
    public PlanetState state;

    public IPlanetBehaviour planetBehaviour;
    public IPlanetAttack planetAttack;
}
