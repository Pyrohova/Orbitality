using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanetAttack : PlanetAttackController
{
    private IEnemyPlanetAI enemyPlanetAI;

    private float maxIntervalBetweenShots = 7f;

    protected override IEnumerator DisableShootUntillCooldownEnds()
    {
        readyToAttackIcon.SetActive(false);
        planetState = PlanetState.OnCooldown;
        yield return new WaitForSeconds(cooldown);

        readyToAttackIcon.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0, maxIntervalBetweenShots));

        planetState = PlanetState.ReadyToAttack;
    }

    public override void Shoot(Vector2 dir)
    {
        rocketManager.CreateRocket(rocketType, gameObject.transform, dir);
        planetController.UpdateCooldown(cooldown);
        StartCoroutine(DisableShootUntillCooldownEnds());
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
