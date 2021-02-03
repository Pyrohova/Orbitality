using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetAttack : MonoBehaviour, IAttackTactik
{
    private RocketManager rocketManager;
    private RocketType rocketType;
    private PlanetState planetState = PlanetState.ReadyToAttack;
    private GameObject readyToAttackIcon;
    private PlanetController planetController;
    private float cooldown;

    private InputController inputController;

    private void Start()
    {
        rocketManager = ServiceLocator.GetInstance().GetRocketManager();
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
        if (planetState == PlanetState.ReadyToAttack)
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            var rocketDirection = (dir - currentPosition).normalized;
            rocketManager.CreateRocket(rocketType, transform, rocketDirection);
            planetController.UpdateCooldown();
            StartCoroutine(DisableShootUntillCooldownEnds());
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
