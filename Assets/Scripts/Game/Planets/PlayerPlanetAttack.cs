using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetAttack : MonoBehaviour, IAttackTactik
{
    private RocketPool rocketPool;
    private RocketType rocketType;
    private PlanetState planetState = PlanetState.ReadyToAttack;
    private GameObject readyToAttackIcon;
    private PlanetController planetController;
    private float cooldown;

    private InputController inputController;

    private void Start()
    {
        rocketPool = ServiceLocator.GetInstance().GetRocketPool();
    }

    private IEnumerator DisableshootUntillCooldownEnds()
    {
        readyToAttackIcon.SetActive(false);
        planetState = PlanetState.OnCooldown;
        yield return new WaitForSeconds(cooldown);
        planetState = PlanetState.ReadyToAttack;
        readyToAttackIcon.SetActive(true);
    }

    public void Shoot(Vector2 dir)
    {
        if (planetState == PlanetState.ReadyToAttack)
        {
            rocketPool.AcquireRocket(rocketType, transform.position, dir);
            planetController.UpdateCooldown();
            StartCoroutine(DisableshootUntillCooldownEnds());
        }
    }

    public void Initialize(RocketType rocketType, float cooldown, GameObject readyToAttackIcon)
    {
        this.rocketType = rocketType;
        this.cooldown = cooldown;
        this.readyToAttackIcon = readyToAttackIcon;

        inputController = ServiceLocator.GetInstance().GetInputController();
        inputController.OnPlayerClick += Shoot;
        planetController = GetComponent<PlanetController>();
    }

}
