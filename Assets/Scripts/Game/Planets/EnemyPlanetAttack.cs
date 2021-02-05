using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows enemy planet to attack other planets using one of strategies.
/// </summary>
public class EnemyPlanetAttack : PlanetAttackController
{
    private IEnemyPlanetAttackStrategy enemyPlanetAI;

    private float maxIntervalBetweenShots = 7f;

    protected override IEnumerator ReloadShooting()
    {
        //disable shooting
        readyToAttackIcon.SetActive(false);
        planetState = PlanetState.OnCooldown;
        yield return new WaitForSeconds(cooldown);

        //make random delay between shots
        readyToAttackIcon.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0, maxIntervalBetweenShots));

        //enable shooting
        planetState = PlanetState.ReadyToAttack;
    }

    public override void Shoot(Vector2 dir)
    {
        rocketManager.CreateRocket(rocketType, gameObject.transform, dir);
        StartCoroutine(ReloadShooting());
    }

    private void FixedUpdate()
    {
        if (planetState == PlanetState.ReadyToAttack)
        {
            var direction = enemyPlanetAI.GetDirection(transform);
            Shoot(direction);
        }
    }

    private void Awake()
    {
        planetController = GetComponent<PlanetController>();
        enemyPlanetAI = ServiceLocator.GetInstance().GetEnemyAIManager().GetRandomEnemyStrategy();

    }
}
