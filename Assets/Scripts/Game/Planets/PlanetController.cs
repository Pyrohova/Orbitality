using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour, IHittable
{
    [SerializeField] private Slider hpSlider;

    [SerializeField] private SpriteRenderer planetImage;
    [SerializeField] private Image readyToShootImage;

    private float speed;
    private float angleCoefficient;
    private Vector3 sunPosition;

    private float currentHP;
    private float maxHP;

    private float reloadingTime;
    private RocketType rocketType;

    private PlanetState state;

    private IPlanetBehaviour planetBehaviour;

    public void Initialize(PlanetInitializationValues values)
    {
        speed = values.speed;
        maxHP = values.maxHP;
        reloadingTime = values.reloadingTime;
        rocketType = values.rocketType;
        planetBehaviour = values.planetBehaviour;
 

        transform.position = values.distanceToSun;
        sunPosition = values.sunPosition;
        angleCoefficient = speed * 360 / values.distanceToSun.x;
        transform.localScale = new Vector2(values.scale, values.scale);
        planetImage.sprite = values.image;

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
        readyToShootImage.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        Move();
    }
}
