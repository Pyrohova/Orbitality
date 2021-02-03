using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private float reloadingTime;

    public Action<float> OnHealthChanged;
    public Action<float> OnCooldownStarted;

    private IAttackTactik attackTactik;

    public void UpdateCooldown()
    {
        OnCooldownStarted?.Invoke(reloadingTime);
    }

    public void Initialize(PlanetInitializationValues values)
    {
        speed = values.speed;
        maxHP = values.maxHP;
        currentHP = maxHP;

        reloadingTime = values.reloadingTime;

        transform.position = values.distanceToSun;
        sunPosition = values.sunPosition;
        angleCoefficient = speed * 360 / values.distanceToSun.x;
        transform.localScale = new Vector2(values.scale, values.scale);
        planetImage.sprite = values.image;

        attackTactik = GetComponent<IAttackTactik>();
        attackTactik.Initialize(values.rocketType, values.reloadingTime, readyToShootImage);

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
    }

    private void FixedUpdate()
    {
        Move();
    }
}
