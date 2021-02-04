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

    private float currentLifetime;
    private Vector3 direction;
    private GameObject nativePlanet;

    private RocketManager rocketManager;
    private SolarSystemManager solarSystemManager;

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
       if (collision.gameObject != nativePlanet)
       {
            var hittable = collision.gameObject.GetComponent<IHittable>();
            hittable.AcceptDamage(damage);
            Explode();
       }
    }

    public void Explode()
    {
        rocketManager.RemoveRocket(this);
    }

    public void Initialize(Vector3 direction, GameObject nativePlanet)
    {
        this.direction = direction;
        this.nativePlanet = nativePlanet;

        transform.position = nativePlanet.transform.position;
        currentLifetime = maxLifetime;
    }

    public void AcceptDamage(float damage)
    {
        Explode();
    }


    private void MoveAlongParabola()
    {
        Vector3 nDir = direction.normalized;

        float angle = Vector2.SignedAngle(nDir, rb.velocity);
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction.normalized);
        transform.RotateAround(transform.position, transform.forward, angle);
        rb.AddForce(nDir * speed);

        List<GameObject> planets = solarSystemManager.GetPlanets();

        foreach (GameObject planet in planets)
        {
            Vector3 forceDirection = planet.transform.position - transform.position;
            if (forceDirection.magnitude < 1)
                continue;
            rb.AddForce(forceDirection.normalized * gravityCoefficient * rb.mass * forceDirection.magnitude / gravityCoefficient);
        }
    }


    private void Move()
    {
        rb.velocity = speed * direction.normalized;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction.normalized);
    }

    private void FixedUpdate()
    {
            currentLifetime -= Time.deltaTime;
            if (currentLifetime > 0)
            {
                Move();
                //MoveAlongParabola();
            }
            else
            {
                Explode();
            }
    }

    private void Awake()
    {
        rb.mass = weight;
        rocketManager = ServiceLocator.GetInstance().GetRocketManager();
        solarSystemManager = ServiceLocator.GetInstance().GetSolarSystemManager();
    }
}
