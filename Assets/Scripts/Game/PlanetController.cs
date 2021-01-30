using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour, IHittable
{
    [Header("Visual")]
    [SerializeField] private Color readyToShootSignColor;
    [SerializeField] private Color shotCooldownSignColor;
    [SerializeField] private SpriteRenderer image;

    [Header("Functional")]
    private float speed;
    private float angleCoefficient;
    private Vector3 sunPosition;

    private float currentHP;
    private float maxHP;
    [SerializeField] private Slider hpSlider;

    private float reloadingTime;
    private RocketType rocketType;

    private PlanetState state;

    private IPlanetBehaviour planetBehaviour;
    private IPlanetAttack planetAttack;

    public void Initialize(PlanetInitializationValues values)
    {
        speed = values.speed;
        maxHP = values.maxHP;
        reloadingTime = values.reloadingTime;
        rocketType = values.rocketType;
        planetBehaviour = values.planetBehaviour;
        planetAttack = values.planetAttack;

        transform.position = values.distanceToSun;
        sunPosition = values.sunPosition;
        angleCoefficient = speed * 360 / values.distanceToSun.x;
        transform.localScale = new Vector2(values.scale, values.scale);
        Debug.Log(values.image.name);
        image.sprite = values.image;

}

    public void AcceptDamage(float damage)
    {
        if (damage >= currentHP)
        {
            Die();
        }
        currentHP -= damage;

        float newValue = currentHP / maxHP;
        hpSlider.value = newValue;
    }

    private void Move()
    {
        float angle = angleCoefficient * Time.deltaTime;

        Vector3 deltaPos = transform.position - sunPosition;
        transform.position = Quaternion.AngleAxis(angle, Vector3.forward) * deltaPos;
    }

    private void Die()
    {
        ServiceLocator.GetInstance().GetSolarSystemManager().DestroyPlanet(gameObject);
    }

    private void Awake()
    {
        currentHP = maxHP;
        hpSlider.value = 1;
        state = PlanetState.ReadyToAttack;
        image.color = readyToShootSignColor;
    }

    private void FixedUpdate()
    {
        Move();
    }
}
