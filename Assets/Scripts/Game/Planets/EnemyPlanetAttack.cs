using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanetAttack : MonoBehaviour, IAttackTactik
{
    private RocketManager rocketManager;
    private RocketType rocketType;
    private PlanetState planetState = PlanetState.ReadyToAttack;
    private GameObject readyToAttackIcon;
    private PlanetController planetController;
    private float cooldown;
    private IEnemyPlanetAI enemyPlanetAI;

    private float maxIntervalBetweenShots = 7f;

    private void Start()
    {
        rocketManager = ServiceLocator.GetInstance().GetRocketManager();
    }

    private IEnumerator DisableShootUntillCooldownEnds()
    {
        readyToAttackIcon.SetActive(false);
        planetState = PlanetState.OnCooldown;
        yield return new WaitForSeconds(cooldown);

        readyToAttackIcon.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0, maxIntervalBetweenShots));

        planetState = PlanetState.ReadyToAttack;
    }

    public void Shoot(Vector2 dir)
    {
        rocketManager.CreateRocket(rocketType, gameObject.transform, dir);
        planetController.UpdateCooldown(cooldown);
        StartCoroutine(DisableShootUntillCooldownEnds());
    }

    public void Initialize(RocketType rocketType, float cooldown, GameObject readyToAttackIcon)
    {
        this.rocketType = rocketType;
        this.cooldown = cooldown;
        this.readyToAttackIcon = readyToAttackIcon;
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
