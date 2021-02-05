using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyShootPlayerPlanet : MonoBehaviour, IEnemyPlanetAttackStrategy
{
    private SolarSystemManager solarSystemManager;

    public Vector2 GetDirection(Transform selectedPlanet)
    {
        Vector2 closestPlanetPosition = GetPlayerPlanetPosition();
        return (closestPlanetPosition - new Vector2(selectedPlanet.transform.position.x, selectedPlanet.transform.position.y)).normalized;
    }

    private Vector2 GetPlayerPlanetPosition()
    {
        var playerPlanet = solarSystemManager.GetPlayerPlanet();

        return playerPlanet.transform.position;
    }

    private void Start()
    {
        solarSystemManager = ServiceLocator.GetInstance().GetSolarSystemManager();
    }
}
