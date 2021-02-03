using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] private StrategyShootClosestPlanet strategyShootClosestPlanet;
    [SerializeField] private StrategyShootPlayerPlanet strategyShootPlayerPlanet;

    private List<IEnemyPlanetAI> enemyPlanetStrategies;

    public StrategyShootClosestPlanet GetStrategyShootClosest()
    {
        return strategyShootClosestPlanet;
    }

    public StrategyShootPlayerPlanet GetStrategyShootPlayer()
    {
        return strategyShootPlayerPlanet;
    }

    public IEnemyPlanetAI GetRandomEnemyStrategy()
    {
        return enemyPlanetStrategies[Random.Range(0, enemyPlanetStrategies.Count)];
    }

    public void Awake()
    {
        enemyPlanetStrategies = new List<IEnemyPlanetAI>() {
            strategyShootClosestPlanet,
            strategyShootPlayerPlanet
        };
    }
}
