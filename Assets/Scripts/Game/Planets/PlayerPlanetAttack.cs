using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows player's planet to attack other planets using player's input.
/// </summary>
public class PlayerPlanetAttack : PlanetAttackController
{
    private InputController inputController;

    private Action<float> OnCooldownStarted;

    protected override IEnumerator ReloadShooting()
    {
        //disable shooting
        readyToAttackIcon.SetActive(false);
        planetState = PlanetState.OnCooldown;
        yield return new WaitForSeconds(cooldown);

        //enable shooting
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
            StartCoroutine(ReloadShooting());

            OnCooldownStarted?.Invoke(cooldown);
        }
    }

    public void Awake()
    {
        inputController = ServiceLocator.GetInstance().GetInputController();
        inputController.OnPlayerClick += Shoot;
        planetController = GetComponent<PlanetController>();

        OnCooldownStarted += (cooldown) => { ServiceLocator.GetInstance().GetGameUIManager().UpdatePlayerCooldownBarValue(cooldown); };
    }

}
