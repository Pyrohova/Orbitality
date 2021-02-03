using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanetAttack : MonoBehaviour, IAttackTactik
{
    private RocketPool rocketPool;
    private RocketType rocketType;
    private PlanetState planetState = PlanetState.ReadyToAttack;
    private GameObject readyToAttackIcon;
    private PlanetController planetController;
    private float cooldown;
    private IEnemyPlanetAI enemyPlanetAI;

    private void Start()
    {
        rocketPool = ServiceLocator.GetInstance().GetRocketPool();
    }

    private IEnumerator DisableShootUntillCooldownEnds()
    {
        readyToAttackIcon.SetActive(false);
        planetState = PlanetState.OnCooldown;
        yield return new WaitForSeconds(cooldown);
        planetState = PlanetState.ReadyToAttack;
        readyToAttackIcon.SetActive(true);
    }

    public void Shoot(Vector2 dir)
    {
        rocketPool.AcquireRocket(rocketType, transform.position, dir);
        planetController.UpdateCooldown();
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
        enemyPlanetAI = ServiceLocator.GetInstance().GetEnemyAIManager().GetStrategyShootClosest();
    }
}
