using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour, IHittable
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float weight;
    [SerializeField] private float maxLifetime;

    [SerializeField] private float cooldown;
    [SerializeField] private RocketType type;

    [SerializeField] private Rigidbody2D rb;

    public bool IsEnabled { get; private set; } = false;

    private float currentLifetime;

    public RocketType GetRocketType()
    {
        return type;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsEnabled)
        {
            var hittable = collision.gameObject.GetComponent<IHittable>();
            hittable.AcceptDamage(damage);
        }
    }

    public void Disable()
    {
        IsEnabled = false;
        ServiceLocator.GetInstance().GetRocketPool().ReleaseRocket(this);
    }

    public void Enable()
    {
        IsEnabled = true;
        currentLifetime = maxLifetime;
    }

    public void AcceptDamage(float damage)
    {
        Disable();
    }

    private void Move()
    {
        
    }

    private void FixedUpdate()
    {
        if (IsEnabled)
        {
            currentLifetime -= Time.deltaTime;
            if (currentLifetime > 0)
            {
                Move();
            }
            else
            {
                Disable();
            }
        }
    }

    private void Awake()
    {
        Disable();
        rb.mass = weight;
    }
}
