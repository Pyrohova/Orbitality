using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetAttack : PlanetAttackController
{
    private InputController inputController;


    protected override IEnumerator DisableShootUntillCooldownEnds()
    {
        readyToAttackIcon.SetActive(false);
        planetState = PlanetState.OnCooldown;
        yield return new WaitForSeconds(cooldown);

        planetState = PlanetState.ReadyToAttack;
        readyToAttackIcon.SetActive(true);
    }

    public override void Shoot(Vector2 dir)
    {
        if (planetState == PlanetState.ReadyToAttack)
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            var rocketDirection = (dir - currentPosition).normalized;
            rocketManager.CreateRocket(rocketType, transform, rocketDirection);
            planetController.UpdateCooldown(cooldown);
            StartCoroutine(DisableShootUntillCooldownEnds());
        }
    }

    public void Awake()
    {
        inputController = ServiceLocator.GetInstance().GetInputController();
        inputController.OnPlayerClick += Shoot;
        planetController = GetComponent<PlanetController>();
    }

}
