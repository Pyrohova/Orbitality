using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls planet's movement and hp.
/// </summary>
public class PlanetController : MonoBehaviour, IHittable
{
    [SerializeField] private Slider hpSlider;

    [SerializeField] private SpriteRenderer planetImage;
    [SerializeField] private GameObject readyToShootImage;

    private float speed;
    private float angleCoefficient;
    private Vector3 sunPosition;

    private float currentHP;
    private float maxHP;

    private Action<float> OnHealthChanged;

    //part that controls current planet attack on other planets
    private PlanetAttackController planetAttackController;

    public void Initialize(PlanetInitializationValues values)
    {
        speed = values.speed;
        maxHP = values.maxHP;
        currentHP = maxHP;

        transform.position = values.distanceToSun;
        sunPosition = values.sunPosition;
        angleCoefficient = speed * 360 / values.distanceToSun.x;
        transform.localScale = new Vector2(values.scale, values.scale);
        planetImage.sprite = values.image;

        planetAttackController = GetComponent<PlanetAttackController>();
        planetAttackController.Initialize(values.rocketType, values.reloadingTime, readyToShootImage);

    }

    public void AcceptDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();

        }
        else
        {
            float newValue = currentHP / maxHP;
            hpSlider.value = newValue;

            OnHealthChanged?.Invoke(newValue);
        }
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
        hpSlider.value = 1;
        readyToShootImage.SetActive(true);

        OnHealthChanged += (cooldown) => { ServiceLocator.GetInstance().GetGameUIManager().UpdatePlayerHealthBarValue(cooldown); };
    }

    private void FixedUpdate()
    {
        Move();
    }
}
