using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyShootClosestPlanet : MonoBehaviour, IEnemyPlanetAI
{
    private SolarSystemManager solarSystemManager;

    public Vector2 GetDirection(Transform selectedPlanet)
    {
        Vector2 closestPlanetPosition = GetClosestPlanetPosition(selectedPlanet);
        return (closestPlanetPosition - new Vector2(selectedPlanet.transform.position.x, selectedPlanet.transform.position.y)).normalized;
    }

    private Vector2 GetClosestPlanetPosition(Transform currentPlanet)
    {
        var allPlanetsExceptCurrent = solarSystemManager.GetAllPlanetsExceptSelected(currentPlanet.gameObject);

        float minDistance = Vector3.Distance(currentPlanet.position, allPlanetsExceptCurrent[0].transform.position);
        Vector3 closestPlanet = Vector3.zero;
        for (int i = 1; i < allPlanetsExceptCurrent.Count; i++)
        {
                float currentDistance = Vector3.Distance(currentPlanet.position, allPlanetsExceptCurrent[i].transform.position);
                if (minDistance > currentDistance)
                {
                    minDistance = currentDistance;
                    closestPlanet = allPlanetsExceptCurrent[i].transform.position;
                }
        }
        return closestPlanet;
    }

    private void Start()
    {
        solarSystemManager = ServiceLocator.GetInstance().GetSolarSystemManager();
    }
}
