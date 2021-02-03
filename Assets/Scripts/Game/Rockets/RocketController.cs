using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour, IHittable
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float weight;
    [SerializeField] private float maxLifetime;
    [SerializeField] private float gravityCoefficient;

    [SerializeField] private float cooldown;
    [SerializeField] private RocketType type;

    [SerializeField] private Rigidbody2D rb;

    public bool IsEnabled { get; private set; } = false;

    private float currentLifetime;

    private Vector3 direction;

    private GameObject nativePlanet;

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
            if (collision.gameObject != nativePlanet)
            {
                var hittable = collision.gameObject.GetComponent<IHittable>();
                hittable.AcceptDamage(damage);
            }
        }
    }

    public void Disable()
    {
        IsEnabled = false;
        ServiceLocator.GetInstance().GetRocketPool().ReleaseRocket(this);
        rb.isKinematic = true;
    }

    public void Enable(Vector3 direction, GameObject nativePlanet)
    {
        this.direction = direction;
        this.nativePlanet = nativePlanet;

        IsEnabled = true;
        currentLifetime = maxLifetime;
        rb.isKinematic = false;
    }

    public void AcceptDamage(float damage)
    {
        Disable();
    }

    private void Move()
    {
        Vector3 curDir = direction;

        float angle = Vector3.SignedAngle(curDir, rb.velocity, transform.forward);
        rb.AddForce(curDir * speed);

        //CalculateGravity();
    }

    private void CalculateGravity()
    {
        List<GameObject> planets = new List<GameObject>();
        planets.AddRange(GameObject.FindGameObjectsWithTag("Planet"));
        planets.Add(GameObject.FindGameObjectWithTag("Sun"));

        foreach (GameObject planet in planets)
        {
            Vector3 forceDirection = planet.transform.position - transform.position;
            if (forceDirection.magnitude < 1) continue;
            rb.AddForce(forceDirection.normalized * gravityCoefficient * rb.mass * forceDirection.magnitude / gravityCoefficient);
        }
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
