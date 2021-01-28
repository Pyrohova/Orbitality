using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour, IHittable
{
    [Header("Visual")]
    public Sprite image;
    public float scale;
    public Color readyToShootSignColor;
    public Color shotCooldownSignColor;

    [Header("Functional")]
    public float speed;
    public float distanceToSun;

    public float currentHP;
    public float maxHP;
    public Slider hpSlider;

    public float reloadingTome;
    public CooldownController cooldownController;
    public PlanetState state;

    public IPlanetBehaviour planetBehaviour;



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

    private void Die()
    {
        // and remove it from solar manager
        Destroy(gameObject);
    }

    private void Awake()
    {
        currentHP = maxHP;
        hpSlider.value = 1;
    }
}
